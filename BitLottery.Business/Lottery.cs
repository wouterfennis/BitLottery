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
        private readonly int _minimalLengthOfBallotNumber;
        private readonly int _maximumLengthOfBallotNumber;
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
            _minimalLengthOfBallotNumber = int.Parse(appsettings["MinimalLengthOfBallotNumber"]);
            _maximumLengthOfBallotNumber = int.Parse(appsettings["MaximumLengthOfBallotNumber"]);
        }

        /// </inheritdoc>
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
            int seed = await generateSeedAsync();

            _randomWrapper.Seed(seed);

            var uniqueBallots = new Dictionary<int, Ballot>();

            for (int i = 0; i < numberOfBallots; i++)
            {
                int ballotNumber = _randomWrapper.Next(_minimalLengthOfBallotNumber, _maximumLengthOfBallotNumber);

                if (!uniqueBallots.ContainsKey(ballotNumber))
                {
                    var ballot = new Ballot
                    {
                        Number = ballotNumber
                    };
                    uniqueBallots.Add(ballotNumber, ballot);
                }
                else
                {
                    i--;
                }
            }

            return uniqueBallots.Values.ToList();
        }

        private async Task<int> generateSeedAsync()
        {
            var settings = new GenerationSettings
            {
                NumberOfIntegers = 1,
                MinimalIntValue = _minimalLengthOfSeed,
                MaximumIntValue = _maximumLengthOfSeed
            };

            IEnumerable<int> randomNumbers = await _randomGenerator.GenerateRandomNumbersAsync(settings);
            return randomNumbers.Single();
        }

        public async Task<Ballot> SellBallotAsync(Draw draw)
        {
            int numberOfBallots = draw.Ballots.Count();

            var generationSettings = new GenerationSettings
            {
                NumberOfIntegers = 1,
                MinimalIntValue = 0,
                MaximumIntValue = numberOfBallots - 1
            };

            IEnumerable<int> randomNumbers = await _randomGenerator.GenerateRandomNumbersAsync(generationSettings);
            int randomIndex = randomNumbers.Single();

            var pickedBallot = draw.Ballots.ElementAt(randomIndex);
            pickedBallot.SellDate = SystemTime.SystemTime.Now();
            return pickedBallot;
        }
    }
}
