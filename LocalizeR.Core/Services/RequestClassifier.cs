using LocalizeR.Core.ServiceContracts;
using LocalizeR_Core;

namespace LocalizeR.Core.Services
{
    public class RequestClassifier : IRequestClassifier
    {
        public async Task<string> ClassifyRequestDetails(string requestDetails)
        {
            //Load sample data
            var sampleData = new RequestClassifierModel.ModelInput()
            {
                REQUEST = requestDetails,
            };

            //Load model and predict output
            var result = RequestClassifierModel.Predict(sampleData);
            string severity = result.PredictedLabel;
            return severity;
        }

    }
}
