using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace CloudOps.DynamoDBv2
{
    public class ListExportsOperation : Operation
    {
        public override string Name => "ListExports";

        public override string Description => "Lists completed exports within the past 90 days.";
 
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
            
            ListExportsResponse resp = new ListExportsResponse();
            do
            {
                ListExportsRequest req = new ListExportsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListExports(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ExportSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}