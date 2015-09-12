using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core.DatatableHelpers.DynamicLinq;

namespace Core.DatatableHelpers
{
    public class DataTablesFilter
    {
        public IQueryable<T> FilterPagingSortingSearch<T>(DataTablesParam dtParameters, IQueryable<T> data, ColInfo[] columns)
        {
            Type type = typeof(T);
            if (!String.IsNullOrEmpty(dtParameters.sSearch))
            {
                var parts = new List<string>();
                var parameters = new List<object>();
                for (var i = 0; i < dtParameters.iColumns; i++)
                {
                    var columnInfo = columns.FirstOrDefault(x => x.Name == dtParameters.mDataProp[i]);
                    if (columnInfo == null)
                        continue;
                    if (!dtParameters.bSearchable[i])
                        continue;
                    if (type.GetProperty(columnInfo.Name) == null
                        && type.GetField(columnInfo.Name) == null)
                    {
                        continue;
                    }
                    try
                    {
                        parts.Add(GetFilterClause(dtParameters.sSearch, columnInfo, parameters));
                    }
                    catch (Exception exception)
                    {
                        Debug.Write(exception.Message);
                    }
                }
                var values = parts.Where(p => p != null);
                if (values.Any())
                {
                    data = data.Where(string.Join(" or ", values), parameters.ToArray());
                }
                else
                {
                    data = data.Where(x => false);
                }
            }

            for (var i = 0; i < dtParameters.sSearchColumns.Count; i++)
            {
                if (!dtParameters.bSearchable[i])
                    continue;

                var searchColumn = dtParameters.sSearchColumns[i];
                if (string.IsNullOrWhiteSpace(searchColumn))
                    continue;

                var parameters = new List<object>();
                var columnInfo = columns.FirstOrDefault(x => x.Name == dtParameters.mDataProp[i]);
                if (columnInfo == null)
                    continue;

                var filterClause = GetFilterClause(dtParameters.sSearchColumns[i], columnInfo, parameters);
                if (string.IsNullOrWhiteSpace(filterClause) == false)
                {
                    data = data.Where(filterClause, parameters.ToArray());
                }
            }
            
            return data;
        }

        delegate string ReturnedFilteredQueryForType(
            string query, string columnName, Type columnType, List<object> parametersForLinqQuery);


        private static readonly List<ReturnedFilteredQueryForType> Filters = new List<ReturnedFilteredQueryForType>()
        {
            Guard(IsBoolType, TypeFilters.BoolFilter),
            Guard(IsDateTimeType, TypeFilters.DateTimeFilter),
            Guard(IsDateTimeOffsetType, TypeFilters.DateTimeOffsetFilter),
            Guard(IsNumericType, TypeFilters.NumericFilter),
            Guard(IsEnumType, TypeFilters.EnumFilter),
            Guard(arg => arg == typeof (string), TypeFilters.StringFilter),
        };


        delegate string GuardedFilter(string query, string columnName, Type columnType, List<object> parametersForLinqQuery);

        private static ReturnedFilteredQueryForType Guard(Func<Type, bool> guard, GuardedFilter filter)
        {
            return (q, c, t, p) => !guard(t)
                                       ? null
                                       : filter(q, c, t, p);
        }

        public static string GetFilterClause(string query, ColInfo column, List<object> parametersForLinqQuery)
        {
            Func<string, string> filterClause = (queryPart) =>
                                                Filters.Select(f =>
                                                               f(queryPart, column.Name, column.Type,
                                                                 parametersForLinqQuery))
                                                       .FirstOrDefault(filterPart => filterPart != null)
                                                ?? "";

            var queryParts = query.Split('|')
                                  .Select(filterClause)
                                  .Where(fc => fc != "")
                                  .ToArray();

            if (queryParts.Any())
            {
                return "(" + string.Join(") OR (", queryParts) + ")";
            }
            return null;
        }


        public static bool IsNumericType(Type type)
        {
            if (type == null || type.IsEnum)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsNumericType(Nullable.GetUnderlyingType(type));
                    }
                    return false;
            }
            return false;

        }
        public static bool IsEnumType(Type type)
        {
            return type.IsEnum;
        }

        public static bool IsBoolType(Type type)
        {
            return type == typeof(bool) || type == typeof(bool?);
        }
        public static bool IsDateTimeType(Type type)
        {
            return type == typeof(DateTime) || type == typeof(DateTime?);
        }
        public static bool IsDateTimeOffsetType(Type type)
        {
            return type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?);
        }

    }
}