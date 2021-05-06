using Amazon;
using Amazon.Athena;
using Amazon.Athena.Model;
using Amazon.Runtime;

namespace CloudOps.Athena
{
    public class ListDatabasesOperation : Operation
    {
        public override string Name => "ListDatabases";

        public override string Description => "Lists the databases in the specified data catalog.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Athena";

        public override string ServiceID => "Athena";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAthenaConfig config = new AmazonAthenaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAthenaClient client = new AmazonAthenaClient(creds, config);
            
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
                    
                    foreach (var obj in resp.DatabaseList)
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