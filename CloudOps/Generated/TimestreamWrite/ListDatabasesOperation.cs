using Amazon;
using Amazon.TimestreamWrite;
using Amazon.TimestreamWrite.Model;
using Amazon.Runtime;

namespace CloudOps.TimestreamWrite
{
    public class ListDatabasesOperation : Operation
    {
        public override string Name => "ListDatabases";

        public override string Description => "Returns a list of your Timestream databases. Service quotas apply. For more information, see Access Management in the Timestream Developer Guide. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "TimestreamWrite";

        public override string ServiceID => "Timestream Write";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonTimestreamWriteConfig config = new AmazonTimestreamWriteConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonTimestreamWriteClient client = new AmazonTimestreamWriteClient(creds, config);
            
            ListDatabasesResponse resp = new ListDatabasesResponse();
            do
            {
                try
                {
                    ListDatabasesRequest req = new ListDatabasesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDatabasesAsync(req);
                    
                    foreach (var obj in resp.Databases)
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