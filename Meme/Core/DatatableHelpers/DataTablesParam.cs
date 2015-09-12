using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Core.DatatableHelpers
{
    public class KeyValue
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class DataTablesParam
    {
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        public int iColumns { get; set; }
        [AllowHtml]
        public string sSearch { get; set; }
        public bool bEscapeRegex { get; set; }
        public int iSortingCols { get; set; }
        public int sEcho { get; set; }
        public List<bool> bSortable { get; set; }
        public List<bool> bSearchable { get; set; }
        public List<string> sSearchColumns { get; set; }
        public List<int> iSortCol { get; set; }
        public List<string> sSortDir { get; set; }
        public List<bool> bEscapeRegexColumns { get; set; }
        public List<string> mDataProp { get; set; }
        public List<KeyValue> filters { get; set; }
        public List<KeyValue> denyFilters { get; set; }
        public bool isSorting { get; set; }
        public string customSorting { get; set; } 
        public string defautSorting { get; set; }

        public bool isUserFilter { get; set; }

        public DataTablesParam()
        {
            bSortable = new List<bool>();
            bSearchable = new List<bool>();
            sSearchColumns = new List<string>();
            iSortCol = new List<int>();
            sSortDir = new List<string>();
            bEscapeRegexColumns = new List<bool>();
            mDataProp = new List<string>();
            filters = new List<KeyValue>();
            denyFilters = new List<KeyValue>();
            isUserFilter = true;
        }

        public string SortColumn()
        {
            if (isSorting)
            {
                // single sort
                var index = iSortCol.FirstOrDefault();
                return index >= 0 ? mDataProp[index] : string.Empty;
            }
            return string.Empty;
        }

        public bool IsSortASC()
        {
            // single sort
            var orderbyDirection = sSortDir.FirstOrDefault();
            if (orderbyDirection == null)
                return true;
            return orderbyDirection == "asc";
        }

        public bool IsSortThenByASC()
        {
            if (sSortDir.Count > 1 && sSortDir[1] != null)
                return sSortDir[1] == "asc";
            // single sort
            var orderbyDirection = sSortDir.FirstOrDefault();
            if (orderbyDirection == null)
                return true;
            return orderbyDirection == "asc";
        }
    }
}