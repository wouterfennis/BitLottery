using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitLottery.Api.Models
{
    public class SaleInfo
    {
        public int CustomerNumber { get; set; }
        public int DrawNumber { get; set; }
        public int? LastNumber { get; set; }
    }
}
