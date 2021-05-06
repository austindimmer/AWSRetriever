using Amazon;
using Amazon.DynamoDB;
using Amazon.DynamoDB.Model;
using Amazon.Runtime;

namespace CloudOps.DynamoDB
{
    public class ListTablesOperation : Operation
    {
        public override string Name => "ListTables";

        public override string Description => "Retrieves a paginated list of table names created by the AWS Account of the caller in the AWS Region (e.g. us-east-1).";
 
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
            
            ListTablesResponse resp = new ListTablesResponse();
            do
            {
                try
                {
                    ListTablesRequest req = new ListTablesRequest
                    {
                        ExclusiveStartTableName = resp.LastEvaluatedTableName
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.ListTablesAsync(req);
                    
                    foreach (var obj in resp.TableNames)
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
            while (!string.IsNullOrEmpty(resp.LastEvaluatedTableName));
        }
    }
}