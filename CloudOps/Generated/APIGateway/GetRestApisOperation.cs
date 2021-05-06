using Amazon;
using Amazon.APIGateway;
using Amazon.APIGateway.Model;
using Amazon.Runtime;

namespace CloudOps.APIGateway
{
    public class GetRestApisOperation : Operation
    {
        public override string Name => "GetRestApis";

        public override string Description => "Lists the RestApis resources for your collection.";
 
        public override string RequestURI => "/restapis";

        public override string Method => "GET";

        public override string ServiceName => "APIGateway";

        public override string ServiceID => "API Gateway";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAPIGatewayConfig config = new AmazonAPIGatewayConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAPIGatewayClient client = new AmazonAPIGatewayClient(creds, config);
            
            GetRestApisResponse resp = new GetRestApisResponse();
            do
            {
                GetRestApisRequest req = new GetRestApisRequest
                {
                    Position = resp.Position
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.GetRestApisAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Items)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Position));
        }
    }
}