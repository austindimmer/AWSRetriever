using Amazon;
using Amazon.AppMesh;
using Amazon.AppMesh.Model;
using Amazon.Runtime;

namespace CloudOps.AppMesh
{
    public class ListGatewayRoutesOperation : Operation
    {
        public override string Name => "ListGatewayRoutes";

        public override string Description => "Returns a list of existing gateway routes that are associated to a virtual gateway.";
 
        public override string RequestURI => "/v20190125/meshes/{meshName}/virtualGateway/{virtualGatewayName}/gatewayRoutes";

        public override string Method => "GET";

        public override string ServiceName => "AppMesh";

        public override string ServiceID => "App Mesh";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppMeshConfig config = new AmazonAppMeshConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppMeshClient client = new AmazonAppMeshClient(creds, config);
            
            ListGatewayRoutesResponse resp = new ListGatewayRoutesResponse();
            do
            {
                try
                {
                    ListGatewayRoutesRequest req = new ListGatewayRoutesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.ListGatewayRoutesAsync(req);
                    
                    foreach (var obj in resp.GatewayRoutes)
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