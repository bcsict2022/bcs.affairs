using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Factories
{
    public class FactorySetupManagerTables : ISetupManagerTables
    {
        private readonly AffairsContext _basedContext;

        public FactorySetupManagerTables(AffairsContext basedContext)
        {
            _basedContext = basedContext;
        }

        public async Task<bool> CreateBetheltypesAsync(vmSetupTable1 vm)
        {
            try
            {
                var departments = new BethelTypes
                {
                    BethelTypeDescription = vm.Description
                };
                await _basedContext.BethelType.AddAsync(departments);
                await _basedContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> CreateDepartmentAsync(vmSetupTable1 vm)
        {
            try
            {
                var departments = new Departments
                {
                   DepartmentDescription = vm.Description
                };
                await _basedContext.Department.AddAsync(departments);
                await _basedContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteBetheltypeAsync(int id)
        {
            try
            {
                var list = await _basedContext.BethelType.FindAsync(id);
                _basedContext.BethelType.Remove(list);
                await _basedContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteDepartmentAsync(int id)
        {
            try
            {
                var list = await _basedContext.Department.FindAsync(id);
                _basedContext.Department.Remove(list);
                await _basedContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EditBetheltypeAsync(BethelTypes vm)
        {
           try
            {
                _basedContext.Entry(vm).State = EntityState.Modified;
                await _basedContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task EditDepartmentAsync(Departments vm)
        {
            try
            {
                _basedContext.Entry(vm).State = EntityState.Modified;
                await _basedContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<BethelTypes>> GetBetheltypesAsync()
        {
            try
            {
                var list = await _basedContext.BethelType.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Countries>> GetCountriesAsync()
        {
            try
            {
                var list = await _basedContext.Country.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<List<Countries>> GetCountriesInRegionAsync(string id)
        //{
        //    try
        //    {
        //        var list = await _basedContext.Country.Where((w) => w.RegionId == id).ToListAsync();
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task<List<Departments>> GetDepartmentsAsync()
        {
            try
            {
                var list = await _basedContext.Department.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Regions>> GetRegionsAsync()
        {
            try
            {
                var list = await _basedContext.Region.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
