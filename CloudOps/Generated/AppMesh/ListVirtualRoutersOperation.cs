using Amazon;
using Amazon.AppMesh;
using Amazon.AppMesh.Model;
using Amazon.Runtime;

namespace CloudOps.AppMesh
{
    public class ListVirtualRoutersOperation : Operation
    {
        public override string Name => "ListVirtualRouters";

        public override string Description => "Returns a list of existing virtual routers in a service mesh.";
 
        public override string RequestURI => "/v20190125/meshes/{meshName}/virtualRouters";

        public override string Method => "GET";

        public override string ServiceName => "AppMesh";

        public override string ServiceID => "App Mesh";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppMeshConfig config = new AmazonAppMeshConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppMeshClient client = new AmazonAppMeshClient(creds, config);
            
            ListVirtualRoutersResponse resp = new ListVirtualRoutersResponse();
            do
            {
                try
                {
                    ListVirtualRoutersRequest req = new ListVirtualRoutersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.ListVirtualRoutersAsync(req);
                    
                    foreach (var obj in resp.VirtualRouters)
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