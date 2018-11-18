using BitLottery.Database.Exceptions;
using BitLottery.Database.Interfaces;
using BitLottery.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BitLottery.Database
{
    public class DrawRepository : IDrawRepository
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
            Draw foundDraw = _context.Draws
                .Include(draw => draw.Ballots)
                .Include(draw => draw.Prices)
                .SingleOrDefault(draw => draw.Number == key);

            if (foundDraw == null)
            {
                throw new EntityNotFoundException($"Drawnumber: {key} not found");
            }
            return foundDraw;
        }

        public int Insert(Draw entity)
        {
            _context.Draws.Add(entity);
            _context.SaveChanges();
            return entity.Number;
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
