using Amazon;
using Amazon.Macie2;
using Amazon.Macie2.Model;
using Amazon.Runtime;

namespace CloudOps.Macie2
{
    public class GetUsageStatisticsOperation : Operation
    {
        public override string Name => "GetUsageStatistics";

        public override string Description => "Retrieves (queries) quotas and aggregated usage data for one or more accounts.";
 
        public override string RequestURI => "/usage/statistics";

        public override string Method => "POST";

        public override string ServiceName => "Macie2";

        public override string ServiceID => "Macie2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacie2Config config = new AmazonMacie2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacie2Client client = new AmazonMacie2Client(creds, config);
            
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