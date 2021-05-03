using Amazon;
using Amazon.LookoutForVision;
using Amazon.LookoutForVision.Model;
using Amazon.Runtime;

namespace CloudOps.LookoutForVision
{
    public class ListProjectsOperation : Operation
    {
        public override string Name => "ListProjects";

        public override string Description => "Lists the Amazon Lookout for Vision projects in your AWS account. This operation requires permissions to perform the lookoutvision:ListProjects operation.";
 
        public override string RequestURI => "/2020-11-20/projects";

        public override string Method => "GET";

        public override string ServiceName => "LookoutForVision";

        public override string ServiceID => "LookoutVision";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLookoutForVisionConfig config = new AmazonLookoutForVisionConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLookoutForVisionClient client = new AmazonLookoutForVisionClient(creds, config);
            
            ListProjectsResponse resp = new ListProjectsResponse();
            do
            {
                ListProjectsRequest req = new ListProjectsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListProjects(req);
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