using System.Web.Mvc;

namespace Core.ModelBinder
{
    public class StringModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var rawValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (rawValue == null || string.IsNullOrWhiteSpace(rawValue.AttemptedValue))
                return null;

            return (rawValue.AttemptedValue + "").Trim();
        }
    }
}