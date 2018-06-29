using BitLottery.RandomService;
using BitLottery.RandomService.Models;
using System.Collections.Generic;

namespace BitLottery.Business.RandomGenerator
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly IRandomService _randomService;

        public RandomGenerator(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public async System.Threading.Tasks.Task<IEnumerable<int>> GenerateRandomNumbersAsync(GenerationSettings settings)
        {
            var serviceSettings = new Settings
            {
                NumberOfIntegers = settings.NumberOfIntegers,
                MinimalIntValue = settings.MinimalIntValue,
                MaximumIntValue = settings.MaximumIntValue
            };

            IEnumerable<int> numbers = await _randomService.GenerateRandomNumbersAsync(serviceSettings);
            return numbers;
        }
    }
}
