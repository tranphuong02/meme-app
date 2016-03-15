using Framework.Datatable.RequestBinder;
using Framework.DI.Contracts.Interfaces;
using Transverse.Models.Business.Account;
using Transverse.Models.Business.User;
using BaseModel = Transverse.Models.Business.BaseModel;

namespace Transverse.Interfaces.Business
{
    public interface IUserBusiness : IDependency
    {
        BaseModel Authenticate(LoginViewModel viewModel);

        BaseModel GetDataForgotPassword(string email);

        BaseModel ValidateToken(string email, string token);

        BaseModel ChangePassword(string email, string token, string newPassword);

        DataTablesResponse GetList(IDataTablesRequest dataTableParam, UserSearchViewModel searchViewModel);

        BaseModel Add(UserAddViewModel viewModel);

        BaseModel Edit(UserEditViewModel viewModel, int id);

        void InitAddViewModel(UserAddViewModel viewModel);

        void InitEditViewModel(UserEditViewModel viewModel, int id);
    }
}