using BitLottery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitLottery.Business
{
    public interface ILottery
    {
        /// <summary>
        /// Generates a list of ballots
        /// </summary>
        Task<IEnumerable<Ballot>> GenerateBallotsAsync(int numberOfBallots);

        /// <summary>
        /// Picks a random ballot from a Draw and registers it as sold
        /// </summary>
        Task<Ballot> SellBallotAsync(Draw draw, int? lastNumber);

        /// <summary>
        /// Pics a random ballot from a Draw and registers it as winning
        /// </summary>
        Task<Draw> DrawWinsAsync(Draw draw);
    }
}
