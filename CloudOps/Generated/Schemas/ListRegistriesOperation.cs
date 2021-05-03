using Amazon;
using Amazon.Schemas;
using Amazon.Schemas.Model;
using Amazon.Runtime;

namespace CloudOps.Schemas
{
    public class ListRegistriesOperation : Operation
    {
        public override string Name => "ListRegistries";

        public override string Description => "List the registries.";
 
        public override string RequestURI => "/v1/registries";

        public override string Method => "GET";

        public override string ServiceName => "Schemas";

        public override string ServiceID => "schemas";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSchemasConfig config = new AmazonSchemasConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSchemasClient client = new AmazonSchemasClient(creds, config);
            
            ListRegistriesResponse resp = new ListRegistriesResponse();
            do
            {
                ListRegistriesRequest req = new ListRegistriesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.ListRegistries(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Registries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}