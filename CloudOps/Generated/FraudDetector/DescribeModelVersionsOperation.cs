using Amazon;
using Amazon.FraudDetector;
using Amazon.FraudDetector.Model;
using Amazon.Runtime;

namespace CloudOps.FraudDetector
{
    public class DescribeModelVersionsOperation : Operation
    {
        public override string Name => "DescribeModelVersions";

        public override string Description => "Gets all of the model versions for the specified model type or for the specified model type and model ID. You can also get details for a single, specified model version. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "FraudDetector";

        public override string ServiceID => "FraudDetector";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonFraudDetectorConfig config = new AmazonFraudDetectorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonFraudDetectorClient client = new AmazonFraudDetectorClient(creds, config);
            
            DescribeModelVersionsResponse resp = new DescribeModelVersionsResponse();
            do
            {
                try
                {
                    DescribeModelVersionsRequest req = new DescribeModelVersionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeModelVersionsAsync(req);
                    
                    foreach (var obj in resp.ModelVersionDetails)
                    {
                        AddObject(obj);
                    }
                    
                }
                catch (System.Exception)
                {
                    CheckError(resp.HttpStatusCode, "200");                
                    throw;
                }

            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}