using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace infrastructure.bcs.affairs.Factories
{
    public class FactoryBethels : IBethels
    {
        private readonly AffairsContext _basedContext;

        public FactoryBethels(AffairsContext basedContext)
        {
            _basedContext = basedContext;
        }

        public async Task<bool> CreateBethelAsync(vmBethel vm)
        {
            try
            {
                var insert = await _basedContext.Database.ExecuteSqlRawAsync("Registration.CreateBethels @BethelTypeId = {0}," +
                    "@BethelDescription = {1},@Address = {2},@Address2 = {3},@CountryId = {4}," +
                    "@StatesProvince = {5},@LocalCouncil = {6},@ZipPostCode = {7},@BcsZone = {8},@Town = {9},@UserId = {10}, @TransactionDate = {11}",
                    vm.BethelTypeId, vm.BethelDescription, vm.Address, vm.Address2, vm.CountryId, vm.StatesProvince, vm.LocalCouncil, vm.ZipPostCode, vm.BcsZone, vm.Town,vm.UserId, DateTime.Today.Date);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteBethelAsync(string id)
        {
            try
            {
                var list = await _basedContext.Bethel.FindAsync(id);
                _basedContext.Bethel.Remove(list);
                await _basedContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task EditBethelAsync(vmBethelEdit vm)
        {
            try
            {
                var bethel = _basedContext.Bethel.Find(vm.Id);
                if(bethel == null)
                {
                    return;
                }
                bethel.BethelTypeId = vm.BethelTypeId;
                bethel.BethelDescription = vm.BethelDescription;
                bethel.Address = vm.Address;
                bethel.Address2 = vm.Address2;
                bethel.CountryId = vm.CountryId;
                bethel.StatesProvince = vm.StatesProvince;
                bethel.LocalCouncil = vm.LocalCouncil;
                bethel.ZipPostCode = vm.ZipPostCode;
                bethel.BcsZone = vm.BcsZone;
                bethel.Town = vm.Town;
                bethel.UserId = vm.UserId;
                bethel.TransactionDate = DateTime.Today;

                await _basedContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<populations>> GetBethelCountsAsync()
        {
            try
            {
                var list = await _basedContext.population.FromSqlRaw("Registration.GetBethelCounts").ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<vmBethelEdit> GetBethelDetailsAsync(string id)
        {
            try
            {
                var lists = await _basedContext.BethelList.Where(w=>w.Id == id)
                    .Select((p) => new vmBethelEdit()
                    {
                        Id = p.Id,
                        BethelDescription = p.BethelDescription,
                        BethelTypeId = p.BethelTypeId,
                        Address = p.Address,
                        Address2 = p.Address2,
                        CountryId = p.CountryId,
                        StatesProvince = p.StatesProvince,
                        LocalCouncil = p.LocalCouncil,
                        ZipPostCode = p.ZipPostCode,
                        BcsZone=p.BcsZone,
                        Town=p.Town,
                        UserId=p.UserId
                    }).ToListAsync();
                return lists.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<BethelDistinctLists>> GetBethelLocalCouncil()
        {
            try
            {
                var list = await _basedContext.Bethel
                                    .Select((p) => new BethelDistinctLists()
                                    {
                                        Description = p.LocalCouncil
                                    }).Distinct().ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<BethelLists>> GetBethelsAsync()
        {
            try
            {
                var lists = await _basedContext.BethelList.ToListAsync();
                return lists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<BethelLists>> GetBethelsInCountryAsync(string countryId)
        {
            try
            {
                var lists = await _basedContext.BethelList.Where((p) => p.CountryId == countryId).ToListAsync();
                return lists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<BethelDistinctLists>> GetBethelStatesProvince()
        {
            try
            {
               var list = await _basedContext.Bethel
                                   .Select((p) => new BethelDistinctLists()
                                   {
                                       Description = p.StatesProvince
                                   }).Distinct().ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<BethelDistinctLists>> GetBethelTown()
        {
            try
            {
                var list = await _basedContext.Bethel
                                    .Select((p) => new BethelDistinctLists()
                                    {
                                        Description = p.Town
                                    }).Distinct().ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
