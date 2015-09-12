using System.Collections.Generic;
using FluentValidation;

namespace Core.Common
{
    public class ValidatorBase<T> : AbstractValidator<T>
    {
        #region Variables

        private static Dictionary<string, string> _titles = new Dictionary<string, string>();
        
        #endregion

        #region Contrusters
        public ValidatorBase()
        {
            ValidatorOptions.ResourceProviderType = typeof(Validator.ValidatorResource);
        }
        #endregion


        #region Helpers

        public string this[string pPropertyName]
        {
            get { return GetTitle(pPropertyName); }
        }

        private string GetTitle(string pPropertyName)
        {
            if (!_titles.ContainsKey(pPropertyName))
                _titles.Add(pPropertyName, "");

            return _titles[pPropertyName];
        }

        #endregion
    }
}
