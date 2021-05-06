using Amazon;
using Amazon.CodePipeline;
using Amazon.CodePipeline.Model;
using Amazon.Runtime;

namespace CloudOps.CodePipeline
{
    public class ListWebhooksOperation : Operation
    {
        public override string Name => "ListWebhooks";

        public override string Description => "Gets a listing of all the webhooks in this AWS Region for this account. The output lists all webhooks and includes the webhook URL and ARN and the configuration for each webhook.";
 
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
            
            ListWebhooksResponse resp = new ListWebhooksResponse();
            do
            {
                ListWebhooksRequest req = new ListWebhooksRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListWebhooksAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Webhooks)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}