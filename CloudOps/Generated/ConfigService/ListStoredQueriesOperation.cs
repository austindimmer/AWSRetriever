using Amazon;
using Amazon.ConfigService;
using Amazon.ConfigService.Model;
using Amazon.Runtime;

namespace CloudOps.ConfigService
{
    public class ListStoredQueriesOperation : Operation
    {
        public override string Name => "ListStoredQueries";

        public override string Description => "Lists the stored queries for a single AWS account and a single AWS Region. The default is 100. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ConfigService";

        public override string ServiceID => "Config Service";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonConfigServiceConfig config = new AmazonConfigServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonConfigServiceClient client = new AmazonConfigServiceClient(creds, config);
            
            ListStoredQueriesResponse resp = new ListStoredQueriesResponse();
            do
            {
                try
                {
                    ListStoredQueriesRequest req = new ListStoredQueriesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListStoredQueriesAsync(req);
                    
                    foreach (var obj in resp.StoredQueryMetadata)
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