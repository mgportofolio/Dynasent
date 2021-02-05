using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynasent.Services;
using Dynasent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dynasent.Pages.Passanger
{
    public class InsertModel : PageModel
    {
        private readonly PassangerService _PassangerMan;
        private readonly BusService _BusMan;

        public InsertModel(PassangerService passangerService, BusService busService)
        {
            this._PassangerMan = passangerService;
            this._BusMan = busService;
        }

        [BindProperty(SupportsGet = true)]
        public PassangerViewModel Form { set; get; }
        [BindProperty(SupportsGet = true)]
        public List<BusViewModel> Bus { set; get; }
       
        [BindProperty(SupportsGet = true)]
        public int BusId { set; get; }

        [BindProperty(SupportsGet = true)]
        public BusViewModel BusViewModel { set; get; }


        public async Task OnGetAsync()
        {
            Bus = await _BusMan.GetDataBus();
            BusId = Form.BusId;
            BusViewModel = await _BusMan.GetBusViewModels(BusId);
        }

        public async Task<IActionResult> OnPostAsync()
        {


            //Form = await _PassangerMan.GetPassangerById(BusId);
            Bus = await _BusMan.GetDataBus();
            BusId = Form.BusId;


            await _PassangerMan.InsertPassanger(new PassangerViewModel
            {
                PassangerName = Form.PassangerName,
                PassangerContact = Form.PassangerContact,
                BusId = BusId,
                QrCodeSrc = "DEFAULT"
            });

            var passangerId = await _PassangerMan.GetPassangerId(Form.PassangerName);
            var passanger = await _PassangerMan.GetPassanger(passangerId);
            var passangerData = JsonConvert.SerializeObject(passanger
               .Where(Q => Q.PassangerId == passangerId)
               .Select(Q => new {Q.PassangerId, Q.PassangerName, Q.BusId})
               .FirstOrDefault());

            await _PassangerMan.InsertQrSrc(passangerId, await
                _PassangerMan.GenerateQrCode(passangerData.ToString()));


            return Redirect("~/Bus/View/"+BusId);
        }
    }
}