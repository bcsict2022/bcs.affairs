using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Repositories
{
    public interface IBandPermissions
    {
        Task<bool> CreateProfiles(vmBand vm);
        Task<List<Bands>> GetProfiles();
        Task EditProfiles(Bands vm);
        Task DeleteProfiles(int key);
        Task<bool> CreateEditUserProfiles(vmUserBand vm);
        Task<UserBandLists> GetUserProfiles(string userId);
    }
}
