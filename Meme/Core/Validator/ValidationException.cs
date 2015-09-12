using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Validator
{
    /// <summary>
    /// Validation Exception
    /// Author: Phuong Tran
    /// Created: 11/09/2015
    /// </summary>
    public class ValidationException : CustomException
    {
        #region Variables

        private static string _errorNotation = "-";

        private Hashtable _errors = new Hashtable();

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="pResult">The p result.</param>
        /// <param name="pObjectError">The p object error.</param>
        public ValidationException(FluentValidation.Results.ValidationResult pResult, dynamic pObjectError)
        {
            _errors.Add(pResult, pObjectError);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="pErrors">The p errors.</param>
        public ValidationException(Hashtable pErrors)
        {
            _errors = pErrors;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <returns></returns>
        public string GetErrorMessage()
        {
            object objKey = null;

            foreach (object key in _errors.Keys)
            {
                objKey = key;
            }

            return GetErrorMessage(objKey);
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="pKey">The p key.</param>
        /// <returns></returns>
        public string GetErrorMessage(object pKey)
        {
            // Empty result
            if (_errors == null || _errors.Count == 0 || pKey == null)
            {
                return null;
            }

            var result = ((FluentValidation.Results.ValidationResult) _errors[pKey]).Errors.Aggregate("", (current, failed) => current + (_errorNotation + " " + failed.ErrorMessage + "\n"));

            // Remove last \n
            result = result.Substring(0, result.Length - 2);

            // Remove 12:00:00 AM
            result = result.Replace("12:00:00 AM", "");

            return result;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <returns></returns>
        public Hashtable GetErrors()
        {
            if (_errors == null)
            {
                return null;
            }

            var table = new Hashtable();

            foreach (dynamic key in _errors.Keys)
            {
                var fieldErrors = new SortedList<string, string>();

                foreach (FluentValidation.Results.ValidationFailure failed in ((FluentValidation.Results.ValidationResult)_errors[key]).Errors)
                {
                    // Empty CustomState
                    if (failed.CustomState == null)
                    {
                        continue;
                    }

                    string fieldName = failed.CustomState.ToString();
                    string result = _errorNotation + " " + failed.ErrorMessage + "\n";

                    // Remove 12:00:00 AM
                    result = result.Replace("12:00:00 AM", "");

                    // Existed
                    if (fieldErrors.ContainsKey(fieldName))
                    {
                        fieldErrors[fieldName] += result;
                    }
                    else // New
                    {
                        fieldErrors.Add(fieldName, result);
                    }
                }

                table.Add(key, fieldErrors);
            }

            return table;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public Hashtable Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        /// <summary>
        /// Gets or sets the error notation.
        /// </summary>
        /// <value>The error notation.</value>
        public static string ErrorNotation
        {
            get { return _errorNotation; }
            set { _errorNotation = value; }
        }

        #endregion
    }
}
