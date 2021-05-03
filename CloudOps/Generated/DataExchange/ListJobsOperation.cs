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

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDataExchangeConfig config = new AmazonDataExchangeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDataExchangeClient client = new AmazonDataExchangeClient(creds, config);
            
            ListJobsResponse resp = new ListJobsResponse();
            do
            {
                ListJobsRequest req = new ListJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListJobs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Jobs)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}