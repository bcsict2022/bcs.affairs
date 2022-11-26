using app.bcs.affairs.APIServices;
using app.bcs.affairs.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using UnifiedFGNBooks.V3.Infrastructure.Repositories;

namespace app.bcs.affairs.Components
{
    public class AppNavigation : ViewComponent
    {
        private readonly IBCSAffairsService _affairsService;

        public AppNavigation(IBCSAffairsService affairsService)
        {
            _affairsService = affairsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int bandId)
        {
            var url = "AffairsNavigations/getProfileMenuItems";
            var bandMenus = await _affairsService.GetAllByIdAsync<MenuCommands[]>(url, bandId.ToString());
          

            return View(bandMenus.ToList());
        }
    }
}
