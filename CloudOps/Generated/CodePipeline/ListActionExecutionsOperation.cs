using Amazon;
using Amazon.CodePipeline;
using Amazon.CodePipeline.Model;
using Amazon.Runtime;

namespace CloudOps.CodePipeline
{
    public class ListActionExecutionsOperation : Operation
    {
        public override string Name => "ListActionExecutions";

        public override string Description => "Lists the action executions that have occurred in a pipeline.";
 
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
            
            ListActionExecutionsResponse resp = new ListActionExecutionsResponse();
            do
            {
                try
                {
                    ListActionExecutionsRequest req = new ListActionExecutionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListActionExecutionsAsync(req);
                    
                    foreach (var obj in resp.ActionExecutionDetails)
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