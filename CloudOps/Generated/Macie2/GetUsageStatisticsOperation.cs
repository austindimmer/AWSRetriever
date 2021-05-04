using Amazon;
using Amazon.Macie;
using Amazon.Macie.Model;
using Amazon.Runtime;

namespace CloudOps.Macie
{
    public class GetUsageStatisticsOperation : Operation
    {
        public override string Name => "GetUsageStatistics";

        public override string Description => "Retrieves (queries) quotas and aggregated usage data for one or more accounts.";
 
        public override string RequestURI => "/usage/statistics";

        public override string Method => "POST";

        public override string ServiceName => "Macie";

        public override string ServiceID => "Macie";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacieConfig config = new AmazonMacieConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacieClient client = new AmazonMacieClient(creds, config);
            
            GetUsageStatisticsResponse resp = new GetUsageStatisticsResponse();
            do
            {
                GetUsageStatisticsRequest req = new GetUsageStatisticsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.GetUsageStatistics(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Records)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}