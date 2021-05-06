using Amazon;
using Amazon.Personalize;
using Amazon.Personalize.Model;
using Amazon.Runtime;

namespace CloudOps.Personalize
{
    public class ListSchemasOperation : Operation
    {
        public override string Name => "ListSchemas";

        public override string Description => "Returns the list of schemas associated with the account. The response provides the properties for each schema, including the Amazon Resource Name (ARN). For more information on schemas, see CreateSchema.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Personalize";

        public override string ServiceID => "Personalize";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPersonalizeConfig config = new AmazonPersonalizeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPersonalizeClient client = new AmazonPersonalizeClient(creds, config);
            
            ListSchemasResponse resp = new ListSchemasResponse();
            do
            {
                try
                {
                    ListSchemasRequest req = new ListSchemasRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListSchemasAsync(req);
                    
                    foreach (var obj in resp.Schemas)
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