using System;

namespace BitLottery.Api.Models
{
    public class DrawConfiguration
    {
        public DateTime SellUntilDate { get; set; }
        public int NumberOfBallots { get; set; }
        public decimal MainPriceAmount { get; set; }
        public decimal FinalNumberPriceAmount { get; set; }
    }
}
