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
    public interface IBethels
    {
        Task<bool> CreateBethelAsync(vmBethel vm);
        Task<List<BethelLists>> GetBethelsAsync();
        Task<List<BethelLists>> GetBethelsInCountryAsync(string countryId);
        Task<vmBethelEdit> GetBethelDetailsAsync(string id);
        Task EditBethelAsync(vmBethelEdit vm);
        Task DeleteBethelAsync(string id);

        Task<List<BethelDistinctLists>> GetBethelStatesProvince();
        Task<List<BethelDistinctLists>> GetBethelLocalCouncil();
        Task<List<BethelDistinctLists>> GetBethelTown();
        Task<List<populations>> GetBethelCountsAsync();
    }
}
