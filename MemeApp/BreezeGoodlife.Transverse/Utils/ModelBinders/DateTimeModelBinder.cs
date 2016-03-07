//////////////////////////////////////////////////////////////////////
// File Name    : DateTimeModelBinder
// System Name  : BreezeGoodlife
// Summary      :
// Author       : phuong.tran
// Change Log   : 12/21/2015 10:46:13 AM - Create Date
/////////////////////////////////////////////////////////////////////

using System;
using System.Globalization;
using System.Web.Mvc;

namespace Transverse.Utils.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var rawValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (string.IsNullOrWhiteSpace(rawValue?.AttemptedValue))
            {
                return null;
            }
            return DateTime.ParseExact(rawValue.AttemptedValue, Framework.Utility.Constants.DateFormat.ddMMyyyy, new CultureInfo("vi", true));
        }
    }
}