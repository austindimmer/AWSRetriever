using Amazon;
using Amazon.AppMesh;
using Amazon.AppMesh.Model;
using Amazon.Runtime;

namespace CloudOps.AppMesh
{
    public class ListTagsForResourceOperation : Operation
    {
        public override string Name => "ListTagsForResource";

        public override string Description => "List the tags for an App Mesh resource.";
 
        public override string RequestURI => "/v20190125/tags";

        public override string Method => "GET";

        public override string ServiceName => "AppMesh";

        public override string ServiceID => "App Mesh";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppMeshConfig config = new AmazonAppMeshConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppMeshClient client = new AmazonAppMeshClient(creds, config);
            
            ListTagsForResourceResponse resp = new ListTagsForResourceResponse();
            do
            {
                ListTagsForResourceRequest req = new ListTagsForResourceRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.ListTagsForResource(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Tags)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}