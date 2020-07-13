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
    public class InsertModel : PageModel
    {
        private readonly BusService _BusMan;
        private readonly DriverService _DriverMan;

       
        [BindProperty(SupportsGet = true)]
        public BusViewModel Form { set; get; }
        [BindProperty(SupportsGet = true)]
        public DriverViewModel FormDriver { set; get; }


        public InsertModel(DriverService driverService, BusService busService)
        {
            this._DriverMan = driverService;
            this._BusMan = busService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            await _DriverMan.InsertDriver(new DriverViewModel
            {
                DriverName = FormDriver.DriverName,
                DriverPhoneNumber = FormDriver.DriverPhoneNumber
            });

            var driverId = await _DriverMan.GetDriverId(FormDriver.DriverName,
                FormDriver.DriverPhoneNumber);

            await _BusMan.InsertBus(new BusViewModel
            {
                BusName = Form.BusName,
                BusNumber = Form.BusNumber,
                BusType = Form.BusType,
                DriversId = driverId
            });

            return RedirectToPage("./Index");
        }
    }
}