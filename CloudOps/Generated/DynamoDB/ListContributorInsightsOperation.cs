using Amazon;
using Amazon.DynamoDB;
using Amazon.DynamoDB.Model;
using Amazon.Runtime;

namespace CloudOps.DynamoDB
{
    public class ListContributorInsightsOperation : Operation
    {
        public override string Name => "ListContributorInsights";

        public override string Description => "Returns a list of ContributorInsightsSummary for a table and all its global secondary indexes.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DynamoDB";

        public override string ServiceID => "DynamoDB";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(creds, config);
            
            ListContributorInsightsResponse resp = new ListContributorInsightsResponse();
            do
            {
                try
                {
                    ListContributorInsightsRequest req = new ListContributorInsightsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListContributorInsightsAsync(req);
                    
                    foreach (var obj in resp.ContributorInsightsSummaries)
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