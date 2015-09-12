using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Core.ModelBinder;

namespace Core.DatatableHelpers
{
    [ModelBinderTarget(typeof(DataTablesParam))]
    public class DataTablesModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var obj = new DataTablesParam();
            var request = controllerContext.HttpContext.Request.Params;
            obj.isSorting = Convert.ToBoolean(request["isSorting"]);
            obj.iDisplayStart = Convert.ToInt32(request["iDisplayStart"]);
            obj.iDisplayLength = Convert.ToInt32(request["iDisplayLength"]);
            obj.iColumns = Convert.ToInt32(request["iColumns"]);
            obj.sSearch = HttpUtility.HtmlDecode((request["sSearch"] + "")).Trim();
            obj.bEscapeRegex = Convert.ToBoolean(request["bEscapeRegex"]);
            obj.iSortingCols = Convert.ToInt32(request["iSortingCols"]);
            obj.sEcho = Convert.ToInt32(request["sEcho"]);

            if (!string.IsNullOrEmpty(request["filters"]))
            {
                obj.filters = Json.Decode<List<KeyValue>>(request["filters"]);
            }
            
            if (!string.IsNullOrEmpty(request["denyFilters"]))
            {
                obj.denyFilters = Json.Decode<List<KeyValue>>(request["denyFilters"]);
            }

            for (var i = 0; i < obj.iColumns; i++)
            {
                obj.bSortable.Add(Convert.ToBoolean(request["bSortable_" + i]));
                obj.bSearchable.Add(Convert.ToBoolean(request["bSearchable_" + i]));
                obj.sSearchColumns.Add(request["sSearch_" + i]);
                obj.bEscapeRegexColumns.Add(Convert.ToBoolean(request["bEscapeRegex_" + i]));
                obj.iSortCol.Add(Convert.ToInt32(request["iSortCol_" + i]));
                obj.sSortDir.Add(request["sSortDir_" + i]);
                obj.mDataProp.Add(request["mDataProp_" + i]);
            }
            return obj;
        }
    }
}
