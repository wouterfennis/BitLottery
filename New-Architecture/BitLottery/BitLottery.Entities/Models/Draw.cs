using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BitLottery.Models
{
    /// <summary>
    /// Entity containing draw information
    /// </summary>
    public class Draw
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The date the draw takes place
        /// </summary>
        public DateTime DrawDate { get; set; }

        /// <summary>
        /// The ballots associated with the draw
        /// </summary>
        public IEnumerable<Ballot> Ballots { get; set; }
    }
}
