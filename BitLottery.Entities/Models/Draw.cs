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

        public void AddPrice(PriceType priceType, decimal amount)
        {
            var price = new Price
            {
                PriceType = priceType,
                Amount = amount
            };

            Prices.Add(price);
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
            return unsoldBallots.Where(ballot => ballot.GetLastDigit() == lastNumber).ToList();
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
        /// Searches for sold ballots with a specific last number
        /// </summary>
        /// <param name="lastNumber">The lastnumber</param>
        /// <returns>All sold ballots with the same lastnumber</returns>
        public IEnumerable<Ballot> GetSoldBallots(int lastNumber)
        {
            var soldBallots = GetSoldBallots();
            return soldBallots.Where(ballot => ballot.GetLastDigit() == lastNumber).ToList();
        }

        /// <summary>
        /// Searches for the main prices
        /// </summary>
        /// <returns>The main price</returns>
        public Price GetMainPrice()
        {
            return Prices.Single(price => price.PriceType == PriceType.Main);
        }

        /// <summary>
        /// Retrieves the final digit price
        /// </summary>
        /// <returns>The final digit price</returns>
        public Price GetFinalDigitPrice()
        {
            return Prices.Single(price => price.PriceType == PriceType.FinalDigit);
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
