using BitLottery.Business.RandomGenerator;
using BitLottery.Business.RandomWrapper;
using BitLottery.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BitLottery.Business
{
  public class Lottery : ILottery
  {
    private readonly IRandomGenerator _randomGenerator;
    private readonly IRandomWrapper _randomWrapper;
    private int _numberOfBallotsInDraw;
    private int _minimalLengthOfBallotNumber;
    private int _maximumLengthOfBallotNumber;

    public Lottery(IRandomGenerator randomGenerator, IRandomWrapper randomWrapper)
    {
      _randomGenerator = randomGenerator;
      _randomWrapper = randomWrapper;

      var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

      var appsettings = config.GetSection("appsettings");
      _numberOfBallotsInDraw = Int32.Parse(appsettings["NumberOfBallotsInDraw"]);
      _minimalLengthOfBallotNumber = Int32.Parse(appsettings["MinimalLengthOfBallotNumber"]);
      _maximumLengthOfBallotNumber = Int32.Parse(appsettings["MaximumLengthOfBallotNumber"]);
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
        MinimalIntValue = _minimalLengthOfBallotNumber,
        MaximumIntValue = _maximumLengthOfBallotNumber
      };

      IEnumerable<int> randomNumbers = await _randomGenerator.GenerateRandomNumbersAsync(settings);
      int seed = randomNumbers.Single();

      _randomWrapper.Seed(seed);

      var ballots = new List<Ballot>();
      for (int i = 0; i < _numberOfBallotsInDraw; i++)
      {
        int ballotNumber = _randomWrapper.Next(10000, 99999);

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
