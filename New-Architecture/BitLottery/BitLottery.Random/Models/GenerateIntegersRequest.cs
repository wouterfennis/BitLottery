using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.RandomService.Models
{
    public class GenerateIntegersRequest
    {
        public string jsonrpc { get; set; }
        public string method { get; set; }
        public GenerateIntegerParams @params { get; set; }
        public int id { get; set; }
    }
}
