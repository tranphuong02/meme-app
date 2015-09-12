using System;

namespace Core.Validator
{
    /// <summary>
    /// Custom Exception
    /// Author: Phuong Tran
    /// Created: 12/09/2015
    /// </summary>
    public class CustomException : Exception
    {
        #region Variables

        #endregion

        #region Contructors

        public CustomException()
            : base()
        {

        }

        public CustomException(string pMessenge)
            : base(pMessenge)
        {

        }

        #endregion

        #region Properties


        #endregion
    }
}
