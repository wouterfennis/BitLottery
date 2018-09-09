using BitLottery.Entities.Models;
using System;
using System.Threading.Tasks;

namespace BitLottery.Business
{
    public interface ILottery
    {
        /// <summary>
        /// Generate and persist a new draw
        /// </summary>
        Task<Draw> GenerateDrawAsync(DateTime drawDate, int numberOfBallots);

        /// <summary>
        /// Picks a random ballot from a Draw and registers it as sold
        /// </summary>
        Task<Ballot> SellBallotAsync(Draw draw, int? lastNumber);

        /// <summary>
        /// Pics a random ballot from a Draw and registers it as winning
        /// </summary>
        Task<Ballot> DrawWinsAsync(Draw draw);
    }
}
