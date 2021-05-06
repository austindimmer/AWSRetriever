using Amazon;
using Amazon.APIGateway;
using Amazon.APIGateway.Model;
using Amazon.Runtime;

namespace CloudOps.APIGateway
{
    public class GetUsagePlansOperation : Operation
    {
        public override string Name => "GetUsagePlans";

        public override string Description => "Gets all the usage plans of the caller&#39;s account.";
 
        public override string RequestURI => "/usageplans";

        public override string Method => "GET";

        public override string ServiceName => "APIGateway";

        public override string ServiceID => "API Gateway";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAPIGatewayConfig config = new AmazonAPIGatewayConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAPIGatewayClient client = new AmazonAPIGatewayClient(creds, config);
            
            GetUsagePlansResponse resp = new GetUsagePlansResponse();
            do
            {
                try
                {
                    GetUsagePlansRequest req = new GetUsagePlansRequest
                    {
                        Position = resp.Position
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.GetUsagePlansAsync(req);
                    
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