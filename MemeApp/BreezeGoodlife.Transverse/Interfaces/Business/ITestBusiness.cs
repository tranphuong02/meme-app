//////////////////////////////////////////////////////////////////////
// File Name    : ITestBusiness
// Summary      :
// Author       : dinh.nguyen
// Change Log   : 11/12/2015 11:15:43 AM - Create Date
/////////////////////////////////////////////////////////////////////

using Framework.DI.Contracts.Interfaces;

namespace Transverse.Interfaces.Business
{
    public interface ITestBusiness : IDependency
    {
        void ExecuteTest();
    }
}