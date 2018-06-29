using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.RandomService.Models
{
    public class RandomResponse
    {
        public IEnumerable<int> data { get; set; }
        public DateTime completionTime { get; set; }
    }
}
