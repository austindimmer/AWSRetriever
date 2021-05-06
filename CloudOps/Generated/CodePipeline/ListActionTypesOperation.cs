using Amazon;
using Amazon.CodePipeline;
using Amazon.CodePipeline.Model;
using Amazon.Runtime;

namespace CloudOps.CodePipeline
{
    public class ListActionTypesOperation : Operation
    {
        public override string Name => "ListActionTypes";

        public override string Description => "Gets a summary of all AWS CodePipeline action types associated with your account.";
 
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
            
            ListActionTypesResponse resp = new ListActionTypesResponse();
            do
            {
                try
                {
                    ListActionTypesRequest req = new ListActionTypesRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListActionTypesAsync(req);
                    
                    foreach (var obj in resp.ActionTypes)
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