using Amazon;
using Amazon.LookoutforVision;
using Amazon.LookoutforVision.Model;
using Amazon.Runtime;

namespace CloudOps.LookoutforVision
{
    public class ListProjectsOperation : Operation
    {
        public override string Name => "ListProjects";

        public override string Description => "Lists the Amazon Lookout for Vision projects in your AWS account. This operation requires permissions to perform the lookoutvision:ListProjects operation.";
 
        public override string RequestURI => "/2020-11-20/projects";

        public override string Method => "GET";

        public override string ServiceName => "LookoutforVision";

        public override string ServiceID => "LookoutVision";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLookoutforVisionConfig config = new AmazonLookoutforVisionConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLookoutforVisionClient client = new AmazonLookoutforVisionClient(creds, config);
            
            ListProjectsResponse resp = new ListProjectsResponse();
            do
            {
                ListProjectsRequest req = new ListProjectsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListProjectsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Projects)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}