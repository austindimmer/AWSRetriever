using Amazon;
using Amazon.Schemas;
using Amazon.Schemas.Model;
using Amazon.Runtime;

namespace CloudOps.Schemas
{
    public class ListDiscoverersOperation : Operation
    {
        public override string Name => "ListDiscoverers";

        public override string Description => "List the discoverers.";
 
        public override string RequestURI => "/v1/discoverers";

        public override string Method => "GET";

        public override string ServiceName => "Schemas";

        public override string ServiceID => "schemas";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSchemasConfig config = new AmazonSchemasConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSchemasClient client = new AmazonSchemasClient(creds, config);
            
            ListDiscoverersResponse resp = new ListDiscoverersResponse();
            do
            {
                ListDiscoverersRequest req = new ListDiscoverersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.ListDiscoverers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Discoverers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}