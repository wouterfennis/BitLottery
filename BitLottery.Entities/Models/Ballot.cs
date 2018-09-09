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
        /// Is this ballot the winner of the draw
        /// </summary>
        public bool IsWinner { get; set; }

        /// <summary>
        /// Registers this ballot as sold
        /// </summary>
        public void RegisterAsSold()
        {
            SellDate = SystemTime.Now();
        }

        /// <summary>
        /// Registers this ballot as the winner
        /// </summary>
        public void RegisterAsWinner()
        {
            IsWinner = true;
        }
    }
}