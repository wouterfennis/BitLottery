using BitLottery.Models;
using System.Linq;

namespace BitLottery.Database
{
    public class BallotRepository : IBallotRepository 
    {
        private readonly BitLotteryContext _context;

        public BallotRepository(BitLotteryContext context)
        {
            _context = context;
        }

        public bool Delete(int key)
        {
            bool deleteSuccessfull = false;

            Ballot foundBallot = Get(key);
            if (foundBallot != null)
            {
                _context.Ballots.Remove(foundBallot);
                _context.SaveChanges();
                deleteSuccessfull = true;
            }
            return deleteSuccessfull;
        }

        public Ballot Get(int key)
        {
            return _context.Ballots
                .SingleOrDefault(ballot => ballot.Id == key);
        }

        public int Insert(Ballot entity)
        {
            _context.Ballots.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public bool Update(Ballot entity, int key)
        {
            bool isUpdateSuccessfull = false;
            Ballot foundBallot = Get(key);
            if (foundBallot != null)
            {
                foundBallot = entity;
                _context.SaveChanges();
                isUpdateSuccessfull = true;
            }
            return isUpdateSuccessfull;
        }
    }
}
