using System;
using AspDotNetLab2.Services.Interfaces;

namespace AspDotNetLab2.Services
{
    public class TimerService : TimerServiceInterface
    {
        private GeneralCounterServiceInterface _generalCounterService;

        public TimerService(GeneralCounterServiceInterface generalCounterService)
        {
            _generalCounterService = generalCounterService;
        }

        public string GetCurrentDate()
        {
            _generalCounterService.IncrementRequests();
            return DateTime.Now.ToString("m/d/yyyy hh:mm:ss");
        }
    }
}
