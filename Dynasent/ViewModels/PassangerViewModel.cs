using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasent.ViewModels
{
    public class PassangerViewModel
    {
        public int PassangerId { set; get; }
        public int BusId { set; get; }
        public string PassangerName { set; get; }
        public string PassangerContact { set; get; }
        public bool InOrOut { set; get; }
        public DateTimeOffset TimeStamp { set; get; }
        public string QrCodeSrc { set; get; }
    }
}
