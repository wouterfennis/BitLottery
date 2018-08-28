using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BitLottery.Models
{
    /// <summary>
    /// Entity containg customer information
    /// </summary>
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The ballots the customer has bought
        /// </summary>
        public IEnumerable<Ballot> Ballots { get; set; }
    }
}