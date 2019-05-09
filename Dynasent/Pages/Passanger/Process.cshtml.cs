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
    public class ProcessModel : PageModel
    {
        private readonly PassangerService _PassangerMan;

        [BindProperty(SupportsGet = true)]
        public PassangerViewModel Form { set; get; }

        public ProcessModel(PassangerService passangerService)
        {
            this._PassangerMan = passangerService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            Form = await _PassangerMan.GetPassangerById(Form.PassangerId);

            if (Form == null)
            {
                return NotFound();
            }

            await _PassangerMan.InOrOutPassanger(Form.PassangerId, Form.BusId, Form.PassangerName);

            return Redirect("~/Bus/View/"+Form.BusId);
            //return Url.Page()
        }
    }
}