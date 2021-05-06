using Amazon;
using Amazon.DataExchange;
using Amazon.DataExchange.Model;
using Amazon.Runtime;

namespace CloudOps.DataExchange
{
    public class ListJobsOperation : Operation
    {
        public override string Name => "ListJobs";

        public override string Description => "This operation lists your jobs sorted by CreatedAt in descending order.";
 
        public override string RequestURI => "/v1/jobs";

        public override string Method => "GET";

        public override string ServiceName => "DataExchange";

        public override string ServiceID => "DataExchange";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDataExchangeConfig config = new AmazonDataExchangeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDataExchangeClient client = new AmazonDataExchangeClient(creds, config);
            
            ListJobsResponse resp = new ListJobsResponse();
            do
            {
                try
                {
                    ListJobsRequest req = new ListJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListJobsAsync(req);
                    
                    foreach (var obj in resp.Jobs)
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