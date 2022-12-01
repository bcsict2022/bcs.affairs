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
    public interface IAffairsNavigations
    {
        Task<List<MenuCommands>> GetProfileMenuItems(int id);
        Task<List<MenuCommands>> GetMenuCommands();
        Task<bool> CreateProfileMenus(ProfileMenus vm);
        //string menuGroup, string menuHeads, string menuLines
        Task<List<GetProfileMenuPermissions>> GetProfileMenuCommands(int profileId);
        //GetDashBoardAggregates GetDashBoardAggregates(string companyId);
        //IEnumerable GetComparisonChartValues(DataSourceLoadOptions loadOptions);
        //IEnumerable GetUserIPDetails(DataSourceLoadOptions loadOptions, string companyId);
        //IEnumerable GetIPAddresses(DataSourceLoadOptions loadOptions, string companyId);
    }
}
