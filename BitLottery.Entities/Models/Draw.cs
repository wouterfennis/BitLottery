using BitLottery.Utilities.SystemTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BitLottery.Models
{
    /// <summary>
    /// Entity containing draw information
    /// </summary>
    public class Draw
    {
        /// <summary>
        /// Database Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The number of the draw
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// After this date no more ballots can be sold
        /// </summary>
        public DateTime SellUntilDate { get; set; }

        /// <summary>
        /// The date the draw takes place
        /// </summary>
        public DateTime? DrawDate { get; set; }

        /// <summary>
        /// The ballots associated with the draw
        /// </summary>
        public IEnumerable<Ballot> Ballots { get; set; }

        /// <summary>
        /// Searches for the unsold ballots
        /// </summary>
        /// <returns>All unsold ballots</returns>
        public IEnumerable<Ballot> GetUnsoldBallots()
        {
            return Ballots.Where(ballot => !ballot.SellDate.HasValue).ToList();
        }

        /// <summary>
        /// Searches for the sold ballots
        /// </summary>
        /// <returns>All sold ballots</returns>
        public IEnumerable<Ballot> GetSoldBallots()
        {
            return Ballots.Where(ballot => ballot.SellDate.HasValue).ToList();
        }

        /// <summary>
        /// Registers this draw as drawn
        /// </summary>
        public void RegisterAsDrawn()
        {
            DrawDate = SystemTime.Now();
        }
    }
}
