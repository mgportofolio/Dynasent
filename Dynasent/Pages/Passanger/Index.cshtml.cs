using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynasent.Services;
using Dynasent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dynasent.Pages.Passanger
{
    public class IndexModel : PageModel
    {
        private readonly PassangerService _PassangerMan;
        private readonly BusService _BusMan;

        [BindProperty(SupportsGet = true)]
        public List<PassangerViewModel> Passangers { set; get; }
        [BindProperty(SupportsGet = true)]
        public List<BusViewModel> Bus { set; get; }

        public IndexModel(PassangerService passangerService, BusService busService)
        {
            this._PassangerMan = passangerService;
            this._BusMan = busService;
        }

        public string BusName(int busId)
        {
            var busName = Bus.Where(Q => Q.BusId == busId)
                .Select(Q => Q.BusName).FirstOrDefault();

            return busName;
        }

        public async Task OnGetAsync()
        {
            Passangers = await _PassangerMan.GetDataPassanger();
            Bus = await _BusMan.GetDataBus();
        }
    }
}