using Amazon;
using Amazon.DynamoDB;
using Amazon.DynamoDB.Model;
using Amazon.Runtime;

namespace CloudOps.DynamoDB
{
    public class ScanOperation : Operation
    {
        public override string Name => "Scan";

        public override string Description => "Retrieves one or more items and its attributes by performing a full scan of a table. Provide a ScanFilter to get more specific results.";
 
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
            
            ScanResponse resp = new ScanResponse();
            do
            {
                try
                {
                    ScanRequest req = new ScanRequest
                    {
                        ExclusiveStartKey = resp.LastEvaluatedKey
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.ScanAsync(req);
                    
                    foreach (var obj in resp.Items)
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
            while (!string.IsNullOrEmpty(resp.LastEvaluatedKey));
        }
    }
}