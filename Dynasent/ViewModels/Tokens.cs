using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasent.ViewModels
{
    public class Tokens
    {
        public int TokenId { set; get; }
        public string TokenType { set; get; }
        public string UniqueCode { set; get; }
        public bool IsExpired { set; get; }
        public DateTimeOffset TimeStamp { set; get; }
    }
}
