using BitLottery.Business.Exceptions;
using BitLottery.Business.RandomGenerator;
using BitLottery.Business.RandomWrapper;
using BitLottery.Entities.Models;
using BitLottery.Utilities.SystemTime;
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
        public async Task<Draw> GenerateDrawAsync(DateTime sellUntilDate, int numberOfBallots)
        {
            IEnumerable<Ballot> ballots = await GenerateBallotsAsync(numberOfBallots);
            var draw = new Draw
            {
                SellUntilDate = sellUntilDate,
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

        public async Task<Ballot> SellBallotAsync(Draw draw, int? lastNumber)
        {
            IEnumerable<Ballot> unsoldBallots = DetermineBallotsToSell(draw, lastNumber);
            DetermineBallotSaleIsPossible(draw, unsoldBallots.Count());

            int numberOfUnsoldBallots = unsoldBallots.Count();
            int indexToBePicked = 0;
            if (numberOfUnsoldBallots != 1)
            {
                indexToBePicked = await GenerateRandomNumberAsync(0, unsoldBallots.Count() - 1);
            }

            var pickedBallot = unsoldBallots.ElementAt(indexToBePicked);
            pickedBallot.RegisterAsSold();
            return pickedBallot;
        }

        private IEnumerable<Ballot> DetermineBallotsToSell(Draw draw, int? lastNumber)
        {
            if (lastNumber.HasValue)
            {
                return draw.GetUnsoldBallots(lastNumber.Value);
            }
            return draw.GetUnsoldBallots();
        }

        private async Task<int> GenerateRandomNumberAsync(int minimalIntValue, int maximumIntValue)
        {
            var generationSettings = new GenerationSettings
            {
                NumberOfIntegers = 1,
                MinimalIntValue = minimalIntValue,
                MaximumIntValue = maximumIntValue
            };

            IEnumerable<int> randomNumbers = await _randomGenerator.GenerateRandomNumbersAsync(generationSettings);
            return randomNumbers.Single();
        }

        private void DetermineBallotSaleIsPossible(Draw draw, int numberOfUnsoldBallots)
        {
            var sellDate = SystemTime.Now();

            if (draw.DrawDate.HasValue)
            {
                throw new DrawException($"This draw has already been drawn at { draw.DrawDate }");
            }

            if (sellDate > draw.SellUntilDate)
            {
                throw new DrawException($"The SellDate: { sellDate } cannot be after the SellUntilDate: { draw.SellUntilDate}");
            }

            if (numberOfUnsoldBallots == 0)
            {
                throw new DrawException("There are no more ballots for sale for this draw");
            }
        }

        public async Task<Ballot> DrawWinsAsync(Draw draw)
        {
            DetermineDrawCanBeDrawn(draw);
            IEnumerable<Ballot> soldBallots = draw.GetSoldBallots();

            int randomIndex = await GenerateRandomNumberAsync(0, soldBallots.Count() - 1);

            var pickedBallot = soldBallots.ElementAt(randomIndex);
            pickedBallot.RegisterAsWinner();
            draw.RegisterAsDrawn();
            return pickedBallot;
        }

        private void DetermineDrawCanBeDrawn(Draw draw)
        {
            int numberOfSoldBallots = draw.GetSoldBallots().Count();
            var drawDate = SystemTime.Now();

            if (draw.DrawDate.HasValue)
            {
                throw new DrawException($"This draw has already been drawn at { draw.DrawDate }");
            }

            if (drawDate < draw.SellUntilDate)
            {
                throw new DrawException($"The DrawDate: { drawDate } cannot be before the SellUntilDate: { draw.SellUntilDate}");
            }

            if (numberOfSoldBallots <= 1)
            {
                throw new DrawException("There are not enough ballots sold for this draw");
            }
        }
    }
}
