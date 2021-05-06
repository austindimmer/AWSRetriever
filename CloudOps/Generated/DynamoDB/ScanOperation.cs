using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace CloudOps.DynamoDBv2
{
    public class ScanOperation : Operation
    {
        public override string Name => "Scan";

        public override string Description => "Retrieves one or more items and its attributes by performing a full scan of a table. Provide a ScanFilter to get more specific results.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DynamoDBv2";

        public override string ServiceID => "DynamoDBv2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(creds, config);
            
            ScanResponse resp = new ScanResponse();
            do
            {
                ScanRequest req = new ScanRequest
                {
                    ExclusiveStartKey = resp.LastEvaluatedKey
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.ScanAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Items)
                {
                    AddObject(obj);
                }
                
            }
            while (resp.LastEvaluatedKey.Count > 0);
        }
    }
}