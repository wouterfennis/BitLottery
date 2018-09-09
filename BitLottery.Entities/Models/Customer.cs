using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BitLottery.Models
{
    /// <summary>
    /// Entity containing customer information
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Database Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The number of the customer
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The ballots the customer has bought
        /// </summary>
        public List<Ballot> Ballots { get; set; }

        public Customer()
        {
            Ballots = new List<Ballot>();
        }

        /// <summary>
        /// Adds a new ballot to the ballots collection
        /// </summary>
        /// <param name="soldBallot">the sold ballot</param>
        public void AddBallot(Ballot soldBallot)
        {
            Ballots.Add(soldBallot);
        }
    }
}