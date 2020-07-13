using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasent.ViewModels
{
    public class BusViewModel
    {
        public int BusId { set; get; }
        public string BusName { set; get; }
        public string BusType { set; get; }
        public string BusNumber { set; get; }
        public int DriversId { set; get; }
        public DateTimeOffset StartTimeStamp { set; get; }
        public bool IsEnd { set; get; }
        public DateTimeOffset? EndTimeStamp { set; get; }
    }
}
