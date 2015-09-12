using System;
using System.Globalization;
using System.Web.Mvc;
using Common.Constants;

namespace Core.ModelBinder
{
    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var rawValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (rawValue == null || string.IsNullOrWhiteSpace(rawValue.AttemptedValue)) return null;

            return DateTime.ParseExact(rawValue.AttemptedValue, AppConstants.DateTimeFormat, new CultureInfo("en-US", true));
        }
    }
}