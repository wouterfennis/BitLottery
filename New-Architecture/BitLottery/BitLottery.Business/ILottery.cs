using BitLottery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitLottery.Business
{
    public interface ILottery
    {
        /// <summary>
        /// Generate and persist a new draw
        /// </summary>
        Task<Draw> GenerateDrawAsync();

        /// <summary>
        /// Retieves ballots from a specific draw with an optional search filter
        /// </summary>
        void RetrieveBallots(int drawId, BallotFilter? ballotFilter);
    }
}
