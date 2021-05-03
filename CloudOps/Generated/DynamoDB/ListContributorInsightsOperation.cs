using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace CloudOps.DynamoDBv2
{
    public class ListContributorInsightsOperation : Operation
    {
        public override string Name => "ListContributorInsights";

        public override string Description => "Returns a list of ContributorInsightsSummary for a table and all its global secondary indexes.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DynamoDBv2";

        public override string ServiceID => "DynamoDBv2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(creds, config);
            
            ListContributorInsightsResponse resp = new ListContributorInsightsResponse();
            do
            {
                ListContributorInsightsRequest req = new ListContributorInsightsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListContributorInsights(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ContributorInsightsSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}