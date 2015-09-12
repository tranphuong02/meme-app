using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Core.DatatableHelpers.DynamicLinq;
using Core.Utils;

namespace Core.DatatableHelpers
{
    public class DataTablesResult : JsonResult
    {
        public static DataTablesResult<TR> Create<T, TR>(IQueryable<T> q, DataTablesParam dataTableParam, Expression<Func<T, bool>> customSearch = null, Expression<Func<T, object>> customSortOrderBy = null, Expression<Func<T, object>> customSortThenBy = null, bool isResultAsQuery = false) where TR : new()
        {
            return new DataTablesResult<T, TR>(q, dataTableParam, customSearch, customSortOrderBy, customSortThenBy, isResultAsQuery);
        }

        public static DataTablesResult<T> Create<T>(IQueryable<T> q, DataTablesParam dataTableParam, Expression<Func<T, bool>> customSearch = null, Expression<Func<T, object>> customSortOrderBy = null, Expression<Func<T, object>> customSortThenBy = null, bool isResultAsQuery = false)
        {
            return new DataTablesResult<T>(q, dataTableParam, customSearch, customSortOrderBy, customSortThenBy, isResultAsQuery);
        }
    }

    public class DataTablesResultMultiThenBy : JsonResult
    {
        public static DataTablesResult<TR> Create<T, TR>(IQueryable<T> q, DataTablesParam dataTableParam, Expression<Func<T, bool>> customSearch = null, Expression<Func<T, object>> customSortOrderBy = null, List<Expression<Func<T, object>>> customSortThenBy = null) where TR : new()
        {
            return new DataTablesResult<T, TR>(q, dataTableParam, customSearch, customSortOrderBy, customSortThenBy);
        }

        public static DataTablesResult<T> Create<T>(IQueryable<T> q, DataTablesParam dataTableParam, Expression<Func<T, bool>> customSearch = null, Expression<Func<T, object>> customSortOrderBy = null, List<Expression<Func<T, object>>> customSortThenBy = null)
        {
            return new DataTablesResult<T>(q, dataTableParam, customSearch, customSortOrderBy, customSortThenBy);
        }
    }

    public class DataTablesResult<T> : DataTablesResult
    {
        public DataTablesResult()
        {

        }

        public DataTablesResult(IQueryable<T> q, DataTablesParam dataTableParam, Expression<Func<T, bool>> customSearch, Expression<Func<T, object>> customSortOrderBy = null, Expression<Func<T, object>> customSortThenBy = null, bool isResultAsQuery = false)
        {
            var content = GetResults(q, dataTableParam, customSearch, customSortOrderBy, customSortThenBy, isResultAsQuery);
            Data = content;
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        public DataTablesResult(IQueryable<T> q, DataTablesParam dataTableParam, Expression<Func<T, bool>> customSearch, Expression<Func<T, object>> customSortOrderBy = null, IEnumerable<Expression<Func<T, object>>> customSortThenBy = null, bool isResultAsQuery = false)
        {
            var content = GetResults(q, dataTableParam, customSearch, customSortOrderBy, customSortThenBy, isResultAsQuery);
            Data = content;
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        DataTablesData GetResults(IQueryable<T> data, DataTablesParam param, Expression<Func<T, bool>> customSearch, Expression<Func<T, object>> customSortOrderBy = null, Expression<Func<T, object>> customSortThenBy = null, bool isResultAsQuery = false)
        {
            var filters = new DataTablesFilter();

            var filteredData = data;

            var searchColumns = TypeExtensions.GetSortedProperties<T>()
                                                .Select(p => new ColInfo(p.Name, p.PropertyType))
                                                .ToArray();

            filteredData = filters.FilterPagingSortingSearch(param, filteredData, searchColumns);

            if (customSearch != null)
            {
                filteredData = filteredData.Union(data.Where(customSearch));
            }

            if (param.isUserFilter && param.filters != null)
            {
                // additional
                foreach (var filter in param.filters)
                {
                    var parameters = new List<object>();
                    var columnInfo = searchColumns.FirstOrDefault(x => x.Name == filter.key);
                    if (columnInfo == null)
                        continue;
                    var filterClause = DataTablesFilter.GetFilterClause(filter.value, columnInfo, parameters);
                    if (string.IsNullOrWhiteSpace(filterClause) == false)
                    {
                        filteredData = filteredData.Where(filterClause, parameters.ToArray());
                    }
                }
            }

            if (customSortOrderBy != null)
            {
                if (customSortThenBy != null)
                {
                    filteredData = param.IsSortASC()
                        ? filteredData.OrderBy(customSortOrderBy).ThenBy(customSortThenBy)
                        : filteredData.OrderByDescending(customSortOrderBy).ThenByDescending(customSortThenBy);
                }
                else
                {
                    filteredData = param.IsSortASC()
                        ? filteredData.OrderBy(customSortOrderBy)
                        : filteredData.OrderByDescending(customSortOrderBy);
                }
            }
            else
            {
                if (param.iSortCol.Any())
                {
                    var sortString = "";
                    for (var i = 0; i < param.iSortingCols; i++)
                    {
                        var columnNumber = param.iSortCol[i];
                        var columModelName = param.mDataProp[columnNumber];
                        var columnName = columModelName;
                        var sortDir = param.sSortDir[i];

                        if (i != 0)
                            sortString += ", ";

                        sortString += columnName + " " + sortDir;
                    }
                    if (!string.IsNullOrWhiteSpace(param.customSorting))
                        sortString = param.customSorting;
                    else if (string.IsNullOrWhiteSpace(sortString))
                        sortString = searchColumns.Any(e => e.Name == "UpdatedDate")
                            ? "UpdatedDate desc, CreatedDate desc"
                            : searchColumns[0].Name;

                    filteredData = filteredData.OrderBy(sortString);
                }
                else
                {
                    filteredData = filteredData.OrderBy(" CreatedDate desc");
                }
            }

            var totalRecords = filteredData.Count();

            var pageData = filteredData.Skip(param.iDisplayStart);
            if (param.iDisplayLength > -1)
            {
                pageData = pageData.Take(param.iDisplayLength);
            }

            var result = new DataTablesData
            {
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                sEcho = param.sEcho,
            };

            if (isResultAsQuery)
                result.aaData = pageData;
            else
                result.aaData = pageData.ToList();
            
            return result;
        }

        DataTablesData GetResults(IQueryable<T> data, DataTablesParam param, Expression<Func<T, bool>> customSearch, Expression<Func<T, object>> customSortOrderBy = null, IEnumerable<Expression<Func<T, object>>> customSortThenBy = null, bool isResultAsQuery = false)
        {
            var filters = new DataTablesFilter();

            var filteredData = data;

            var searchColumns = TypeExtensions.GetSortedProperties<T>()
                                                .Select(p => new ColInfo(p.Name, p.PropertyType))
                                                .ToArray();

            filteredData = filters.FilterPagingSortingSearch(param, filteredData, searchColumns);

            if (customSearch != null)
            {
                filteredData = filteredData.Union(data.Where(customSearch));
            }

            if (param.isUserFilter && param.filters != null)
            {
                // additional
                foreach (var filter in param.filters)
                {
                    var parameters = new List<object>();
                    var columnInfo = searchColumns.FirstOrDefault(x => x.Name == filter.key);
                    if (columnInfo == null)
                        continue;
                    var filterClause = DataTablesFilter.GetFilterClause(filter.value, columnInfo, parameters);
                    if (string.IsNullOrWhiteSpace(filterClause) == false)
                    {
                        filteredData = filteredData.Where(filterClause, parameters.ToArray());
                    }
                }
            }

            if (customSortOrderBy != null)
            {
                if (customSortThenBy != null)
                {
                    var filteredDataOrdered = param.IsSortASC()
                        ? filteredData.OrderBy(customSortOrderBy)
                        : filteredData.OrderByDescending(customSortOrderBy);

                    foreach (var expression in customSortThenBy)
                    {
                        filteredDataOrdered = param.IsSortThenByASC()
                            ? filteredDataOrdered.ThenBy(expression)
                            : filteredDataOrdered.ThenByDescending(expression);
                    }
                    filteredData = filteredDataOrdered;
                }
                else
                {
                    filteredData = param.IsSortASC()
                        ? filteredData.OrderBy(customSortOrderBy)
                        : filteredData.OrderByDescending(customSortOrderBy);
                }
            }
            else
            {
                if (param.iSortCol.Any())
                {
                    var sortString = "";
                    for (var i = 0; i < param.iSortingCols; i++)
                    {
                        var columnNumber = param.iSortCol[i];
                        var columModelName = param.mDataProp[columnNumber];
                        var columnName = columModelName;
                        var sortDir = param.sSortDir[i];

                        if (i != 0)
                            sortString += ", ";

                        sortString += columnName + " " + sortDir;
                    }
                    if (!string.IsNullOrWhiteSpace(param.customSorting))
                        sortString = param.customSorting;
                    else
                        if (string.IsNullOrWhiteSpace(sortString))
                            sortString = searchColumns.Any(e => e.Name == "UpdatedDate") ? "UpdatedDate desc, CreatedDate desc" : searchColumns[0].Name;

                    filteredData = filteredData.OrderBy(sortString);
                }
            }

            var totalRecords = filteredData.Count();

            var pageData = filteredData.Skip(param.iDisplayStart);
            if (param.iDisplayLength > -1)
            {
                pageData = pageData.Take(param.iDisplayLength);
            }

            var result = new DataTablesData
            {
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                sEcho = param.sEcho,
            };

            if (isResultAsQuery)
                result.aaData = pageData;
            else
                result.aaData = pageData.ToList();

            return result;
        }
    }

    public class DataTablesResult<T, TRessult> : DataTablesResult<TRessult> where TRessult : new()
    {
        public DataTablesResult(IQueryable<T> q, DataTablesParam dataTableParam, Expression<Func<T, bool>> customSearch, Expression<Func<T, object>> customSortOrderBy = null, Expression<Func<T, object>> customSortThenBy = null, bool isResultAsQuery = false)
        {
            var content = GetResults(q, dataTableParam, customSearch, customSortOrderBy, customSortThenBy, isResultAsQuery);
            Data = content;
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        public DataTablesResult(IQueryable<T> q, DataTablesParam dataTableParam, Expression<Func<T, bool>> customSearch, Expression<Func<T, object>> customSortOrderBy = null, IEnumerable<Expression<Func<T, object>>> customSortThenBy = null, bool isResultAsQuery = false)
        {
            var content = GetResults(q, dataTableParam, customSearch, customSortOrderBy, customSortThenBy, isResultAsQuery);
            Data = content;
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        DataTablesData GetResults(IQueryable<T> data, DataTablesParam param, Expression<Func<T, bool>> customSearch, Expression<Func<T, object>> customSortOrderBy = null, Expression<Func<T, object>> customSortThenBy = null, bool isResultAsQuery = false)
        {
            var filters = new DataTablesFilter();

            var filteredData = data;

            var searchColumns = TypeExtensions.GetSortedProperties<T>()
                                              .Select(p => new ColInfo(p.Name, p.PropertyType))
                                              .ToArray();

            filteredData = filters.FilterPagingSortingSearch(param, filteredData, searchColumns);

            if (customSearch != null)
            {
                filteredData = filteredData.Union(data.Where(customSearch));
            }

            if (param.isUserFilter && param.filters != null)
            {
                // additional
                foreach (var filter in param.filters)
                {
                    var parameters = new List<object>();
                    var columnInfo = searchColumns.FirstOrDefault(x => x.Name == filter.key);
                    if (columnInfo == null)
                        continue;
                    var filterClause = DataTablesFilter.GetFilterClause(filter.value, columnInfo, parameters);
                    if (string.IsNullOrWhiteSpace(filterClause) == false)
                    {
                        filteredData = filteredData.Where(filterClause, parameters.ToArray());
                    }
                }
            }

            if (customSortOrderBy != null)
            {
                if (customSortThenBy != null)
                {
                    filteredData = param.IsSortASC()
                        ? filteredData.OrderBy(customSortOrderBy).ThenBy(customSortThenBy)
                        : filteredData.OrderByDescending(customSortOrderBy).ThenByDescending(customSortThenBy);
                }
                else
                {
                    filteredData = param.IsSortASC()
                        ? filteredData.OrderBy(customSortOrderBy)
                        : filteredData.OrderByDescending(customSortOrderBy);
                }
            }
            else
            {
                if (param.iSortCol.Any())
                {
                    var sortString = "";
                    for (var i = 0; i < param.iSortingCols; i++)
                    {
                        var columnNumber = param.iSortCol[i];
                        var columModelName = param.mDataProp[columnNumber];
                        var columnName = columModelName;
                        var sortDir = param.sSortDir[i];

                        if (i != 0)
                            sortString += ", ";

                        sortString += columnName + " " + sortDir;
                    }
                    if (!string.IsNullOrWhiteSpace(param.customSorting))
                        sortString = param.customSorting;
                    else
                        if (string.IsNullOrWhiteSpace(sortString))
                            sortString = searchColumns.Any(e => e.Name == "UpdatedDate") ? "UpdatedDate desc, CreatedDate desc" : searchColumns[0].Name;

                    filteredData = filteredData.OrderBy(sortString);
                }
            }

            var totalRecords = filteredData.Count();

            var pageData = filteredData.Skip(param.iDisplayStart);
            if (param.iDisplayLength > -1)
            {
                pageData = pageData.Take(param.iDisplayLength);
            }

            var result = new DataTablesData
            {
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                sEcho = param.sEcho,
            };

            if (isResultAsQuery)
                result.aaData = pageData;
            else
                result.aaData = ObjectTransform.EntitiesToModels<T, TRessult>(pageData.ToList());

            return result;
        }

        DataTablesData GetResults(IQueryable<T> data, DataTablesParam param, Expression<Func<T, bool>> customSearch, Expression<Func<T, object>> customSortOrderBy = null, IEnumerable<Expression<Func<T, object>>> customSortThenBy = null, bool isResultAsQuery = false)
        {
            var filters = new DataTablesFilter();

            var filteredData = data;

            var searchColumns = TypeExtensions.GetSortedProperties<T>()
                                              .Select(p => new ColInfo(p.Name, p.PropertyType))
                                              .ToArray();

            filteredData = filters.FilterPagingSortingSearch(param, filteredData, searchColumns);

            if (customSearch != null)
            {
                filteredData = filteredData.Union(data.Where(customSearch));
            }

            if (param.isUserFilter && param.filters != null)
            {
                // additional
                foreach (var filter in param.filters)
                {
                    var parameters = new List<object>();
                    var columnInfo = searchColumns.FirstOrDefault(x => x.Name == filter.key);
                    if (columnInfo == null)
                        continue;
                    var filterClause = DataTablesFilter.GetFilterClause(filter.value, columnInfo, parameters);
                    if (string.IsNullOrWhiteSpace(filterClause) == false)
                    {
                        filteredData = filteredData.Where(filterClause, parameters.ToArray());
                    }
                }
            }

            if (customSortOrderBy != null)
            {
                if (customSortThenBy != null)
                {
                    var filteredDataOrdered = param.IsSortASC()
                        ? filteredData.OrderBy(customSortOrderBy)
                        : filteredData.OrderByDescending(customSortOrderBy);

                    foreach (var expression in customSortThenBy)
                    {
                        filteredDataOrdered = param.IsSortThenByASC()
                            ? filteredDataOrdered.ThenBy(expression)
                            : filteredDataOrdered.ThenByDescending(expression);
                    }
                    filteredData = filteredDataOrdered;
                }
                else
                {
                    filteredData = param.IsSortASC()
                        ? filteredData.OrderBy(customSortOrderBy)
                        : filteredData.OrderByDescending(customSortOrderBy);
                }
            }
            else
            {
                if (param.iSortCol.Any())
                {
                    var sortString = "";
                    for (var i = 0; i < param.iSortingCols; i++)
                    {
                        var columnNumber = param.iSortCol[i];
                        var columModelName = param.mDataProp[columnNumber];
                        var columnName = columModelName;
                        var sortDir = param.sSortDir[i];

                        if (i != 0)
                            sortString += ", ";

                        sortString += columnName + " " + sortDir;
                    }
                    if (!string.IsNullOrWhiteSpace(param.customSorting))
                        sortString = param.customSorting;
                    else
                        if (string.IsNullOrWhiteSpace(sortString))
                            sortString = searchColumns.Any(e => e.Name == "UpdatedDate") ? "UpdatedDate desc, CreatedDate desc" : searchColumns[0].Name;

                    filteredData = filteredData.OrderBy(sortString);
                }
            }

            var totalRecords = filteredData.Count();

            var pageData = filteredData.Skip(param.iDisplayStart);
            if (param.iDisplayLength > -1)
            {
                pageData = pageData.Take(param.iDisplayLength);
            }

            var result = new DataTablesData
            {
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                sEcho = param.sEcho,
            };

            if (isResultAsQuery)
                result.aaData = pageData;
            else
                result.aaData = ObjectTransform.EntitiesToModels<T, TRessult>(pageData.ToList());

            return result;
        }
    }

    public class ColInfo
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        public ColInfo(string name, Type propertyType)
        {
            Name = name;
            Type = propertyType;

        }
    }
}