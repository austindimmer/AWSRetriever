using Amazon;
using Amazon.Neptune;
using Amazon.Neptune.Model;
using Amazon.Runtime;

namespace CloudOps.Neptune
{
    public class ListTagsForResourceOperation : Operation
    {
        public override string Name => "ListTagsForResource";

        public override string Description => "Lists all tags on an Amazon Neptune resource.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Neptune";

        public override string ServiceID => "Neptune";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonNeptuneConfig config = new AmazonNeptuneConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonNeptuneClient client = new AmazonNeptuneClient(creds, config);
            
            ListTagsForResourceResponse resp = new ListTagsForResourceResponse();
            ListTagsForResourceRequest req = new ListTagsForResourceRequest
            {                    
                                    
            };
            resp = await client.ListTagsForResourceAsync(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.TagList)
            {
                AddObject(obj);
            }
            
        }
    }
}