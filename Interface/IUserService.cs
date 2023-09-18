﻿using SaleSavvy_API.Models;
using SaleSavvy_API.Models.GetUser;
using SaleSavvy_API.Models.GetUser.Entity;
using SaleSavvy_API.Models.Login;
using SaleSavvy_API.Models.UpdateUser;

namespace SaleSavvy_API.Interface
{
    public interface IUserService
    {
        Task<GetUserEntity[]> GetListUser();
        Task<OutputUpdateUser> DeleteUser(string id);
        Task<OutputUpdateUser> ModifyUserData(InputUpdateUser input);
    }
}