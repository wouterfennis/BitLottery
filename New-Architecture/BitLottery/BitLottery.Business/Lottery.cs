using BitLottery.Business.RandomGenerator;
using BitLottery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitLottery.Business
{
  public class Lottery : ILottery
  {
    private readonly IRandomGenerator _randomGenerator;

    public Lottery(IRandomGenerator randomGenerator)
    {
      _randomGenerator = randomGenerator;
    }

    public async Task<Draw> GenerateDrawAsync()
    {
      IEnumerable<Ballot> ballots = await GenerateBallotsAsync();
      var draw = new Draw
      {
        Ballots = ballots
      };

      return draw;
    }

    private async Task<IEnumerable<Ballot>> GenerateBallotsAsync()
    {
      var settings = new GenerationSettings
      {
        NumberOfIntegers = 1,
        MinimalIntValue = 1000000,
        MaximumIntValue = 10000000
      };

      IEnumerable<int> randomNumbers = await _randomGenerator.GenerateRandomNumbersAsync(settings);
      int seed = randomNumbers.First();

      var random = new Random(seed);

      var ballots = new List<Ballot>();
      for (int i = 0; i < 10; i++)
      {
        int ballotNumber = random.Next(10000, 99999);

        var ballot = new Ballot
        {
          Number = ballotNumber
        };
        ballots.Add(ballot);
      }

      return ballots;
    }

    public void RetrieveBallots(int drawId, BallotFilter? ballotFilter)
    {
      throw new NotImplementedException();
    }
  }
}
