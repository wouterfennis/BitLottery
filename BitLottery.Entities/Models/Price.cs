using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BitLottery.Entities.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }

        public PriceType PriceType { get; set; }

        public decimal Amount { get; set; }
    }
}
