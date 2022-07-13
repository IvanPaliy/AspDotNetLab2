using System;
using AspDotNetLab2.Services.Interfaces;

namespace AspDotNetLab2.Services
{
    public class RandomService : RandomServiceInterface
    {
        private GeneralCounterServiceInterface _generalCounterService;
        private static Random _random;
        private double _value;
        public RandomService(GeneralCounterServiceInterface generalCounterService)
        {
            _generalCounterService = generalCounterService;
            _random = new Random();
            _value = Math.Round(_random.NextDouble(), 3);
            _generalCounterService.IncrementRequests();
        }
        public double RandomValue => _value;
    }
}