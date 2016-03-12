﻿using Framework.DI.Contracts.Interfaces;
using Transverse.Models.Business;
using Transverse.Models.Business.Account;

namespace Transverse.Interfaces.Business
{
    public interface IUserBusiness : IDependency
    {
        BaseModel Authenticate(LoginViewModel viewModel);
        BaseModel GetDataForgotPassword(string email);
        BaseModel ValidateToken(string email, string token);
        BaseModel ChangePassword(string email, string token, string newPassword);
    }
}