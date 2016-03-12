using System;
using Framework.Logger.Log4Net;
using Framework.Utility;

namespace Transverse.Utils
{
    public class ConfigEmailHelpers
    {
        public MailHelper NewEmail()
        {
            try
            {
                var mailHelper = new MailHelper(
                    BackendHelpers.Email(),
                    BackendHelpers.Password(),
                    BackendHelpers.EnableSsl(),
                    BackendHelpers.Host(),
                    BackendHelpers.Port())
                {
                    Sender = BackendHelpers.Email()
                };

                return mailHelper;
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);

                return new MailHelper(BackendHelpers.Email(), BackendHelpers.Password());
            }
        }
    }
}
