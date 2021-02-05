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
    public class DeleteModel : PageModel
    {
        private readonly PassangerService _PassangerMan;

        [BindProperty(SupportsGet = true)]
        public PassangerViewModel Form { set; get; }

        public DeleteModel(PassangerService passangerService)
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

            var busId = Form.BusId;

            await _PassangerMan.DeletePassanger(Form.PassangerId);

            return Redirect("~/Bus/View/"+busId);
        }
    }
}