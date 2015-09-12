using System;
using System.Globalization;
using System.Web.Mvc;

namespace Core.ModelBinder
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                if (valueResult != null && !string.IsNullOrEmpty(valueResult.AttemptedValue))
                {
                    var usCulture = new CultureInfo("en-US");
                    actualValue = Math.Round(Convert.ToDecimal(valueResult.AttemptedValue.Replace(",", "."), usCulture), 3);
                }
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}