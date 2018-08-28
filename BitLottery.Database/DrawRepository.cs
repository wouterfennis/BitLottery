using BitLottery.Models;
using System;

namespace BitLottery.Database
{
    public class DrawRepository : IRepository<Draw, int>
    {
        private readonly BitLotteryContext _context;

        public DrawRepository(BitLotteryContext context)
        {
            _context = context;
        }

        public bool Delete(int key)
        {
            bool deleteSuccessfull = false;

            Draw foundDraw = Get(key);
            if (foundDraw != null)
            {
                _context.Draws.Remove(foundDraw);
                _context.SaveChanges();
                deleteSuccessfull = true;
            }
            return deleteSuccessfull;
        }

        public Draw Get(int key)
        {
            return _context.Draws.Find(key);
        }

        public int Insert(Draw entity)
        {
            _context.Draws.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public bool Update(Draw entity, int key)
        {
            bool isUpdateSuccessfull = false;
            Draw foundDraw = Get(key);
            if (foundDraw != null)
            {
                foundDraw = entity;
                _context.SaveChanges();
                isUpdateSuccessfull = true;
            }
            return isUpdateSuccessfull;
        }
    }
}
