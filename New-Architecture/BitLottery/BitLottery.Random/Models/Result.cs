using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.RandomService.Models
{
    class Result
    {
        public RandomResponse random { get; set; }
        public int bitsUsed { get; set; }
        public int bitsLeft { get; set; }
        public int requestsLeft { get; set; }
        public int advisoryDelay { get; set; }
    }
}