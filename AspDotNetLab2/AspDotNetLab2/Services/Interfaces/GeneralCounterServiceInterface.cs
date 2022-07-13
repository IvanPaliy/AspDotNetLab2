namespace AspDotNetLab2.Services.Interfaces
{
    public interface GeneralCounterServiceInterface
    {
        int RequestsValue { get; }
        void IncrementRequests();
    }
}
