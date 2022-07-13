using AspDotNetLab2.Services.Interfaces;

namespace AspDotNetLab2.Services
{
    public class GeneralCounterService : GeneralCounterServiceInterface
    {
        private int _value;
        public GeneralCounterService()
        {
            _value = 0;
        }
        public int RequestsValue => _value;
        public void IncrementRequests()
        {
            _value += 1;
        }
    }
}
