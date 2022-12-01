using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Repositories
{
    public interface IUsers
    {
        Task<bool> CreateUserAsync(vmUser vm);
        Task<List<vmUserDetails>> GetUsersAsync();
        Task EditUserAsync(vmEditUser vm);
        Task DeleteUserAsync(string id);

        Task<UserNameDetails> GetLogin(vmLogin vm);
        Task<bool> LoginTransactions(vmLoginTransaction vm);
        Task<vmLoginUserProfile> GetUserProfile(string id);
        Task<bool> CreateUserIPDetails(UserIPDetails vm);

        Task<bool> EditUserPasswords(vmPasswordChange vm);
    }
}
