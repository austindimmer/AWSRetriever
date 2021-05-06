using Amazon;
using Amazon.CodePipeline;
using Amazon.CodePipeline.Model;
using Amazon.Runtime;

namespace CloudOps.CodePipeline
{
    public class ListTagsForResourceOperation : Operation
    {
        public override string Name => "ListTagsForResource";

        public override string Description => "Gets the set of key-value pairs (metadata) that are used to manage the resource.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodePipeline";

        public override string ServiceID => "CodePipeline";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodePipelineConfig config = new AmazonCodePipelineConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodePipelineClient client = new AmazonCodePipelineClient(creds, config);
            
            ListTagsForResourceResponse resp = new ListTagsForResourceResponse();
            do
            {
                try
                {
                    ListTagsForResourceRequest req = new ListTagsForResourceRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListTagsForResourceAsync(req);
                    
                    foreach (var obj in resp.Tags)
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