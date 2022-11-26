using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Repositories
{
    public interface ISetupManagerTables
    {
        Task<List<Countries>> GetCountriesAsync();

        Task<bool> CreateDepartmentAsync(vmSetupTable1 vm);
        Task<List<Departments>> GetDepartmentsAsync();
        Task EditDepartmentAsync(Departments vm);
        Task DeleteDepartmentAsync(int id);


        Task<bool> CreateBetheltypesAsync(vmSetupTable1 vm);
        Task<List<BethelTypes>> GetBetheltypesAsync();
        Task EditBetheltypeAsync(BethelTypes vm);
        Task DeleteBetheltypeAsync(int id);
    }
}
