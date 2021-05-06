using Amazon;
using Amazon.DynamoDB;
using Amazon.DynamoDB.Model;
using Amazon.Runtime;

namespace CloudOps.DynamoDB
{
    public class QueryOperation : Operation
    {
        public override string Name => "Query";

        public override string Description => "Gets the values of one or more items and its attributes by primary key (composite primary key, only). Narrow the scope of the query using comparison operators on the RangeKeyValue of the composite key. Use the ScanIndexForward parameter to get results in forward or reverse order by range key.";
 
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
            
            QueryResponse resp = new QueryResponse();
            do
            {
                try
                {
                    QueryRequest req = new QueryRequest
                    {
                        ExclusiveStartKey = resp.LastEvaluatedKey
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.QueryAsync(req);
                    
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