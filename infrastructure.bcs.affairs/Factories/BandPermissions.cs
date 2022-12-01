using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Factories
{
    public class FactoryBandPermissions : IBandPermissions
    {
        private readonly AffairsContext _basedContext;

        public FactoryBandPermissions(AffairsContext basedContext)
        {
            _basedContext = basedContext;
        }
        public async Task<bool> CreateEditUserProfiles(vmUserBand vm)
        {
            try
            {
                var up = await _basedContext.UserBand.FindAsync(vm.UserId); //.Where(w => w.UserId == vm.UserId).ToListAsync();
                if (up != null)
                {
                    //var userProfiles = _basedContext.UserBand.First((o) => o.UserId == up.FirstOrDefault().UserId);
                    up.BandId = vm.BandId;
                    await _basedContext.SaveChangesAsync();
                }
                else
                {
                    var profile = new UserBands
                    {
                        UserId = vm.UserId,
                        BandId = vm.BandId
                    };
                   await _basedContext.UserBand.AddAsync(profile);
                   await _basedContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> CreateProfiles(vmBand vm)
        {
            try
            {
                var profile = new Bands
                {
                    Name = vm.GroupName
                };
                await _basedContext.Band.AddAsync(profile);
                await _basedContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteProfiles(int key)
        {
            try
            {
                var list = await _basedContext.Band.FindAsync(key);
                _basedContext.Band.Remove(list);
                await _basedContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task EditProfiles(Bands vm)
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
        public async Task<List<Bands>> GetProfiles()
        {
            try
            {
                var lists = await _basedContext.Band.ToListAsync();
                return lists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserBandLists> GetUserProfiles(string userId)
        {
            try
            {
                var band = await _basedContext.UserBandList.FindAsync(userId);
                if (band != null){
                    
                    return band;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
