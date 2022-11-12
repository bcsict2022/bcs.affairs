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
    public class FactorySetupRegistrationTables : ISetupRegistrationTables
    {
        private readonly AffairsContext _basedContext;

        public FactorySetupRegistrationTables(AffairsContext basedContext)
        {
            _basedContext = basedContext;
        }

        public async Task<bool> CreateBethelTypeAsync(vmSetupTable1 vm)
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
        public async Task DeleteBethelTypeAsync(int id)
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
        public async Task EditBethelTypeAsync(BethelTypes vm)
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
        public async Task<List<BethelTypes>> GetBethelTypesAsync()
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
    }
}
