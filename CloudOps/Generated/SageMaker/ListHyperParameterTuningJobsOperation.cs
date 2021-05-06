using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListHyperParameterTuningJobsOperation : Operation
    {
        public override string Name => "ListHyperParameterTuningJobs";

        public override string Description => "Gets a list of HyperParameterTuningJobSummary objects that describe the hyperparameter tuning jobs launched in your account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SageMaker";

        public override string ServiceID => "SageMaker";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSageMakerConfig config = new AmazonSageMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSageMakerClient client = new AmazonSageMakerClient(creds, config);
            
            ListHyperParameterTuningJobsResponse resp = new ListHyperParameterTuningJobsResponse();
            do
            {
                try
                {
                    ListHyperParameterTuningJobsRequest req = new ListHyperParameterTuningJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListHyperParameterTuningJobsAsync(req);
                    
                    foreach (var obj in resp.HyperParameterTuningJobSummaries)
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