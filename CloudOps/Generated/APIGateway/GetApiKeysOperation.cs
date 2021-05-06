using Amazon;
using Amazon.APIGateway;
using Amazon.APIGateway.Model;
using Amazon.Runtime;

namespace CloudOps.APIGateway
{
    public class GetApiKeysOperation : Operation
    {
        public override string Name => "GetApiKeys";

        public override string Description => "Gets information about the current ApiKeys resource.";
 
        public override string RequestURI => "/apikeys";

        public override string Method => "GET";

        public override string ServiceName => "APIGateway";

        public override string ServiceID => "API Gateway";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAPIGatewayConfig config = new AmazonAPIGatewayConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAPIGatewayClient client = new AmazonAPIGatewayClient(creds, config);
            
            GetApiKeysResponse resp = new GetApiKeysResponse();
            do
            {
                try
                {
                    GetApiKeysRequest req = new GetApiKeysRequest
                    {
                        Position = resp.Position
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.GetApiKeysAsync(req);
                    
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
            while (!string.IsNullOrEmpty(resp.Position));
        }
    }
}