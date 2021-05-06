using Amazon;
using Amazon.MachineLearning;
using Amazon.MachineLearning.Model;
using Amazon.Runtime;

namespace CloudOps.MachineLearning
{
    public class DescribeEvaluationsOperation : Operation
    {
        public override string Name => "DescribeEvaluations";

        public override string Description => "Returns a list of DescribeEvaluations that match the search criteria in the request.";
 
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
            
            DescribeEvaluationsResponse resp = new DescribeEvaluationsResponse();
            do
            {
                try
                {
                    DescribeEvaluationsRequest req = new DescribeEvaluationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.DescribeEvaluationsAsync(req);
                    
                    foreach (var obj in resp.Results)
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