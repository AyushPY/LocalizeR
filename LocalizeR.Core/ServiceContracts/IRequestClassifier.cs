namespace LocalizeR.Core.ServiceContracts
{
    public interface IRequestClassifier
    {
        Task<string> ClassifyRequestDetails(string requestDetails);
    }
}
