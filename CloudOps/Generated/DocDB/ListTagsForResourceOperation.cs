using Amazon;
using Amazon.DocDB;
using Amazon.DocDB.Model;
using Amazon.Runtime;

namespace CloudOps.DocDB
{
    public class ListTagsForResourceOperation : Operation
    {
        public override string Name => "ListTagsForResource";

        public override string Description => "Lists all tags on an Amazon DocumentDB resource.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DocDB";

        public override string ServiceID => "DocDB";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDocDBConfig config = new AmazonDocDBConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDocDBClient client = new AmazonDocDBClient(creds, config);
            
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