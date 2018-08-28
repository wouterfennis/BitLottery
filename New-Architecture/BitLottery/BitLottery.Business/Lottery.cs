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
    private int _minimalLengthOfBallotNumber;
    private int _maximumLengthOfBallotNumber;
    private int _minimalLengthOfSeed;
    private int _maximumLengthOfSeed;

    public Lottery(IRandomGenerator randomGenerator, IRandomWrapper randomWrapper)
    {
      _randomGenerator = randomGenerator;
      _randomWrapper = randomWrapper;

      var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

      var appsettings = config.GetSection("appsettings");
      _minimalLengthOfSeed = int.Parse(appsettings["MinimalLengthOfSeed"]);
      _maximumLengthOfSeed = int.Parse(appsettings["MaximumLengthOfSeed"]);
      _maximumLengthOfBallotNumber = int.Parse(appsettings["MaximumLengthOfBallotNumber"]);
      _maximumLengthOfBallotNumber = int.Parse(appsettings["MaximumLengthOfBallotNumber"]);
    }

    public async Task<Draw> GenerateDrawAsync(DateTime drawDate, int numberOfBallots)
    {
      IEnumerable<Ballot> ballots = await GenerateBallotsAsync(numberOfBallots);
      var draw = new Draw
      {
        DrawDate = drawDate,
        Ballots = ballots
      };

      return draw;
    }

    private async Task<IEnumerable<Ballot>> GenerateBallotsAsync(int numberOfBallots)
    {
      var settings = new GenerationSettings
      {
        NumberOfIntegers = 1,
        MinimalIntValue = _minimalLengthOfSeed,
        MaximumIntValue = _maximumLengthOfSeed
      };

      IEnumerable<int> randomNumbers = await _randomGenerator.GenerateRandomNumbersAsync(settings);
      int seed = randomNumbers.Single();

      _randomWrapper.Seed(seed);

      var ballots = new List<Ballot>();
      for (int i = 0; i < numberOfBallots; i++)
      {
        int ballotNumber = _randomWrapper.Next(_minimalLengthOfBallotNumber, _maximumLengthOfBallotNumber);

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
