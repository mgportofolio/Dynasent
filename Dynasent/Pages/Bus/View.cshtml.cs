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
    public class ViewModel : PageModel
    {
        private readonly PassangerService _PassangerMan;
        private readonly BusService _BusMan;

        [BindProperty(SupportsGet = true)]
        public List<PassangerViewModel> Passangers { set; get; }

        [BindProperty(SupportsGet = true)]
        public List<BusViewModel> BusVW { set; get; }

        [BindProperty(SupportsGet = true)]
        public PassangerViewModel Form { set; get; }
    
        [BindProperty(SupportsGet = true)]
        public DriverViewModel Drivers { set; get; }

        [BindProperty(SupportsGet = true)]
        public BusViewModel BusInfo { set; get; }

        [BindProperty(SupportsGet = true)]
        public int BusId { set; get; }


        public ViewModel(PassangerService passangerService, BusService busService)
        {
            this._PassangerMan = passangerService;
            this._BusMan = busService;
        }

        public string BusName(int busId)
        {
            var busName = BusVW.Where(Q => Q.BusId == busId)
                .Select(Q => Q.BusName).FirstOrDefault();

            return busName;
        }

        public async Task OnGetAsync()
        {
            BusVW = await _BusMan.GetDataBus();
            BusId = Form.BusId;
       
            Form = await _PassangerMan.GetPassangerById(BusId);
            var passangerData = await _PassangerMan.GetDataPassanger();
            if (Form == null)
            {
                Passangers = passangerData;
            }


            Passangers = passangerData.Where(Q => Q.BusId == BusId)
                .Select(Q => Q).ToList();

            Drivers = await _BusMan.GetDriverViewModels(BusId);
            BusInfo = await _BusMan.GetBusViewModels(BusId);

            BusVW = BusVW.Where(Q => Q.BusId == BusId).Select(Q => Q).ToList();
           
        }
    }
}