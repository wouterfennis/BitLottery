using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.Database.UnitTests
{
    [TestClass]
    public class RepositoryTestsBase
    {
        public DbContextOptions<BitLotteryContext> Options;

        [TestInitialize]
        public void Initialize()
        {
            Options = new DbContextOptionsBuilder<BitLotteryContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
        }
    }
}
