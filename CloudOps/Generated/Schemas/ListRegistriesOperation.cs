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

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSchemasConfig config = new AmazonSchemasConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSchemasClient client = new AmazonSchemasClient(creds, config);
            
            ListRegistriesResponse resp = new ListRegistriesResponse();
            do
            {
                try
                {
                    ListRegistriesRequest req = new ListRegistriesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.ListRegistriesAsync(req);
                    
                    foreach (var obj in resp.Registries)
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