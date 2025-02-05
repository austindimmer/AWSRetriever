using Amazon;
using Amazon.AppMesh;
using Amazon.AppMesh.Model;
using Amazon.Runtime;

namespace CloudOps.AppMesh
{
    public class ListVirtualGatewaysOperation : Operation
    {
        public override string Name => "ListVirtualGateways";

        public override string Description => "Returns a list of existing virtual gateways in a service mesh.";
 
        public override string RequestURI => "/v20190125/meshes/{meshName}/virtualGateways";

        public override string Method => "GET";

        public override string ServiceName => "AppMesh";

        public override string ServiceID => "App Mesh";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppMeshConfig config = new AmazonAppMeshConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppMeshClient client = new AmazonAppMeshClient(creds, config);
            
            ListVirtualGatewaysResponse resp = new ListVirtualGatewaysResponse();
            do
            {
                ListVirtualGatewaysRequest req = new ListVirtualGatewaysRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.ListVirtualGateways(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.VirtualGateways)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}