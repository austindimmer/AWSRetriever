using Amazon;
using Amazon.MachineLearning;
using Amazon.MachineLearning.Model;
using Amazon.Runtime;

namespace CloudOps.MachineLearning
{
    public class DescribeBatchPredictionsOperation : Operation
    {
        public override string Name => "DescribeBatchPredictions";

        public override string Description => "Returns a list of BatchPrediction operations that match the search criteria in the request.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "MachineLearning";

        public override string ServiceID => "Machine Learning";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMachineLearningConfig config = new AmazonMachineLearningConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMachineLearningClient client = new AmazonMachineLearningClient(creds, config);
            
            DescribeBatchPredictionsResponse resp = new DescribeBatchPredictionsResponse();
            do
            {
                DescribeBatchPredictionsRequest req = new DescribeBatchPredictionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.DescribeBatchPredictionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Results)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}