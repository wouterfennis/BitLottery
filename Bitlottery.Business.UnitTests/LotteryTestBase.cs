using BitLottery.Business;
using BitLottery.Business.RandomGenerator;
using BitLottery.Business.RandomWrapper;
using BitLottery.Utilities.SystemTime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BitLottery.Business.UnitTests
{
    [TestClass]
    public class LotteryTestBase
    {
        public Mock<IRandomGenerator> RandomGeneratorMock { get; set; }
        public Mock<IRandomWrapper> RandomWrapperMock { get; set; }
        public Lottery Lottery { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            RandomGeneratorMock = new Mock<IRandomGenerator>(MockBehavior.Strict);
            RandomWrapperMock = new Mock<IRandomWrapper>(MockBehavior.Strict);

            Lottery = new Lottery(RandomGeneratorMock.Object, RandomWrapperMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            SystemTime.ResetDateTime();
        }
    }
}
