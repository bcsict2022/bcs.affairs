using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Models;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace infrastructure.bcs.affairs.Factories
{
    public class FactoryAffairsNavigations : IAffairsNavigations
    {
        private readonly AffairsContext _basedContext;

        public FactoryAffairsNavigations(AffairsContext basedContext)
        {
            _basedContext = basedContext;
        }
        public async Task<bool> CreateProfileMenus(string menuGroup, string menuHeads, string menuLines)
        {
            try
            {
                int groupmenu = await _basedContext.Database.ExecuteSqlRawAsync("Manager.CreateBandMenuPermissions @BandId = {0},@MenuHeads = {1},@MenuLines = {2}", menuGroup, menuHeads, menuLines);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<MenuCommands>> GetMenuCommands()
        {
            try
            {
                var menuCommands = await _basedContext.MenuCommand.ToListAsync();              
                return menuCommands;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<GetProfileMenuPermissions>> GetProfileMenuCommands(int profileId)
        {
            try
            {
                var menus = await _basedContext.GetProfileMenuPermission.FromSqlRaw("Manager.GetBandMenuPermissions @GroupId = {0}", profileId).ToListAsync();
              
                return menus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<MenuCommands>> GetProfileMenuItems(int id)
        {
            try
            {
                var menus = await _basedContext.MenuCommand.FromSqlRaw("Manager.GetBandMenus @Id = {0}", id).ToListAsync();
                return menus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
