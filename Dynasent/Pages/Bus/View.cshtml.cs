using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynasent.Api;
using Dynasent.Services;
using Dynasent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dynasent.Pages.Bus
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ViewModel : PageModel
    {
        private readonly PassangerService _PassangerMan;
        private readonly BusService _BusMan;

        public class AllProperty
        {
            public List<PassangerViewModel> Passangers { set; get; }
            public List<BusViewModel> BusList { set; get; }
            public DriverViewModel Drivers { set; get; }
            public BusViewModel BusInfo { set; get; }
            public int BusId { set; get; }
            public int CountIn { set; get; }
            public int CountOut { set; get; }
        }

        [BindProperty(SupportsGet = true)]
        public AllProperty Property { set; get; }

        public ViewModel(PassangerService passangerService, BusService busService)
        {
            this._PassangerMan = passangerService;
            this._BusMan = busService;
        }

        public string BusName(int busId)
        {
            var busName = Property.BusList.Where(Q => Q.BusId == busId)
                .Select(Q => Q.BusName).FirstOrDefault();

            return busName;
        }

        
        public async Task<IActionResult> OnGetAsync()
        {

            Property.BusList = await _BusMan.GetDataBus();

            var passangerData = await _PassangerMan.GetDataPassanger();
            Property.Passangers = passangerData.Where(Q => Q.BusId == Property.BusId)
                .Select(Q => Q).OrderBy(Q => Q.BusId).ToList();

            if (Property.Passangers == null)
            {
                return NotFound();
            }

            Property.Drivers = await _BusMan.GetDriverViewModels(Property.BusId);
            Property.BusInfo = await _BusMan.GetBusViewModels(Property.BusId);

            Property.BusList = Property.BusList.Where(Q => Q.BusId == Property.BusId).Select(Q => Q).ToList();
            if (Property.BusList == null)
            {
                return NotFound();
            }

            Property.CountIn = Property.Passangers.Where(Q => Q.InOrOut == true)
                .Select(Q => Q).Count();
            Property.CountOut = Property.Passangers.Where(Q => Q.InOrOut == false)
                .Select(Q => Q).Count();

            //Response.Headers.Add("Refresh", "10");  
            
            return Page();
        }
    }
}