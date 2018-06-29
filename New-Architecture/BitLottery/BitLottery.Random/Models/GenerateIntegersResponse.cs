using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.RandomService.Models
{
    class GenerateIntegersResponse
    {
        public string jsonrpc { get; set; }
        public Result result { get; set; }
        public int Id { get; set; }
    }
}