using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynasent.Services;
using Dynasent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dynasent.Pages.Bus
{
    public class IndexModel : PageModel
    {
        private readonly PassangerService _PassangerMan;
        private readonly BusService _BusMan;
        private readonly DriverService _DriverMan;

        public IndexModel(PassangerService passangerService, BusService busService, 
            DriverService driverService)
        {
            this._PassangerMan = passangerService;
            this._BusMan = busService;
            this._DriverMan = driverService;
        }

        [BindProperty(SupportsGet = true)]
        public List<PassangerViewModel> Passangers { set; get; }
        [BindProperty(SupportsGet = true)]
        public List<BusViewModel> Bus { set; get; }
        [BindProperty(SupportsGet = true)]
        public List<DriverViewModel> Drivers { set; get; }

        public async Task<int> CountPassangerAsync(int busId)
        {
            Passangers = await _PassangerMan.GetDataPassanger();
            var passangerCount = Passangers.Where(Q => Q.BusId == busId)
                .Select(Q=>Q)
                .Count();
            return passangerCount;
        }

        public async Task<string> GetDriverName(int driverId)
        {
            Drivers = await _DriverMan.GetDataDriver();
            var driverName = Drivers.Where(Q => Q.DriversId == driverId)
                .Select(Q => Q.DriverName).FirstOrDefault();
            return driverName;
        }

        public async Task<string> GetDriverNumber(int driverId)
        {
            Drivers = await _DriverMan.GetDataDriver();
            var driverNumber = Drivers.Where(Q => Q.DriversId == driverId)
                .Select(Q => Q.DriverPhoneNumber).FirstOrDefault();
            return driverNumber;
        }

        public async Task OnGetAsync()
        {
            Drivers = await _DriverMan.GetDataDriver();
            Bus = await _BusMan.GetDataBus();
            Passangers = await _PassangerMan.GetDataPassanger();

        }
    }
}