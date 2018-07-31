using BitLottery.RandomService.Models;

namespace BitLottery.RandomService
{
  public interface IBuilder
  {
    GenerateIntegersRequest Build();
  }
}