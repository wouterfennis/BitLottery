using BitLottery.Utilities.SystemTime;
using System;
using System.ComponentModel.DataAnnotations;

namespace BitLottery.Entities.Models
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

        /// <summary>
        /// The price this ballot has won
        /// </summary>
        public Price WonPrice { get; set; }

        /// <summary>
        /// Registers this ballot as sold
        /// </summary>
        public void RegisterAsSold()
        {
            SellDate = SystemTime.Now();
        }

        /// <summary>
        /// Extracts last digit of number
        /// </summary>
        /// <returns>The last digit of the number</returns>
        public int GetLastDigit()
        {
            return Number % 10;
        }
    }
}