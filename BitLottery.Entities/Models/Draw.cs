using BitLottery.Entities.Models;
using BitLottery.Utilities.SystemTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BitLottery.Entities.Models
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
        /// The prices associated with the draw
        /// </summary>
        public List<Price> Prices { get; set; }

        public Draw()
        {
            Ballots = new List<Ballot>();
            Prices = new List<Price>();
        }

        /// <summary>
        /// Searches for unsold ballots
        /// </summary>
        /// <returns>All unsold ballots</returns>
        public IEnumerable<Ballot> GetUnsoldBallots()
        {
            return Ballots.Where(ballot => !ballot.SellDate.HasValue).ToList();
        }

        /// <summary>
        /// Searches for unsold ballots with a specific last number
        /// </summary>
        /// <param name="lastNumber">The lastnumber</param>
        /// <returns>All unsold ballots with the same lastnumber</returns>
        public IEnumerable<Ballot> GetUnsoldBallots(int lastNumber)
        {
            var unsoldBallots = GetUnsoldBallots();
            return unsoldBallots.Where(ballot => GetLastDigit(ballot.Number) == lastNumber).ToList();
        }

        private int GetLastDigit(int number)
        {
            return number % 10;
        }

        /// <summary>
        /// Searches for sold ballots
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
