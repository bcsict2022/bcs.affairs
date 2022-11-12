using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Repositories
{
    public interface ISetupRegistrationTables
    {
        Task<bool> CreateBethelTypeAsync(vmSetupTable1 vm);
        Task<List<BethelTypes>> GetBethelTypesAsync();
        Task EditBethelTypeAsync(BethelTypes vm);
        Task DeleteBethelTypeAsync(int id);
    }
}
