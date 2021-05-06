using Amazon;
using Amazon.CodePipeline;
using Amazon.CodePipeline.Model;
using Amazon.Runtime;

namespace CloudOps.CodePipeline
{
    public class ListPipelinesOperation : Operation
    {
        public override string Name => "ListPipelines";

        public override string Description => "Gets a summary of all of the pipelines associated with your account.";
 
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
            
            ListPipelinesResponse resp = new ListPipelinesResponse();
            do
            {
                try
                {
                    ListPipelinesRequest req = new ListPipelinesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListPipelinesAsync(req);
                    
                    foreach (var obj in resp.Pipelines)
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