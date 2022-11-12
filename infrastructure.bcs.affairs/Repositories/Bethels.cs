using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Repositories
{
    public interface IBethels
    {
        Task<bool> CreateBethelAsync(vmBethel vm);
        Task<List<BethelLists>> GetBethelsAsync();
        Task<List<BethelLists>> GetBethelsAsync(string countryId);
        Task<List<BethelLists>> GetBethelDetailsAsync(string id);
        Task EditBethelAsync(Bethels vm);
        Task DeleteBethelAsync(string id);
    }
}
