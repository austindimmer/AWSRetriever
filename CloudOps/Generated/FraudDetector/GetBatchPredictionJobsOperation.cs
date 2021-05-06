using Amazon;
using Amazon.FraudDetector;
using Amazon.FraudDetector.Model;
using Amazon.Runtime;

namespace CloudOps.FraudDetector
{
    public class GetBatchPredictionJobsOperation : Operation
    {
        public override string Name => "GetBatchPredictionJobs";

        public override string Description => "Gets all batch prediction jobs or a specific job if you specify a job ID. This is a paginated API. If you provide a null maxResults, this action retrieves a maximum of 50 records per page. If you provide a maxResults, the value must be between 1 and 50. To get the next page results, provide the pagination token from the GetBatchPredictionJobsResponse as part of your request. A null pagination token fetches the records from the beginning.";
 
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
            
            GetBatchPredictionJobsResponse resp = new GetBatchPredictionJobsResponse();
            do
            {
                try
                {
                    GetBatchPredictionJobsRequest req = new GetBatchPredictionJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.GetBatchPredictionJobsAsync(req);
                    
                    foreach (var obj in resp.BatchPredictions)
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