using BitLottery.Utilities.SystemTime;
using System;
using System.ComponentModel.DataAnnotations;

namespace BitLottery.Models
{
    /// <summary>
    /// Entity containing ballot information
    /// </summary>
    public class Ballot
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The DateTime the ballot was sold
        /// </summary>
        public DateTime? SellDate { get; set; }

        /// <summary>
        /// The ballot number
        /// </summary>
        public int Number { get; set; }

        public void Sell()
        {
            SellDate = SystemTime.Now();
        }
    }
}