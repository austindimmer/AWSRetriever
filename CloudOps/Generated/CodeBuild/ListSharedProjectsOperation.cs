using Amazon;
using Amazon.CodeBuild;
using Amazon.CodeBuild.Model;
using Amazon.Runtime;

namespace CloudOps.CodeBuild
{
    public class ListSharedProjectsOperation : Operation
    {
        public override string Name => "ListSharedProjects";

        public override string Description => " Gets a list of projects that are shared with other AWS accounts or users. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeBuild";

        public override string ServiceID => "CodeBuild";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeBuildConfig config = new AmazonCodeBuildConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeBuildClient client = new AmazonCodeBuildClient(creds, config);
            
            ListSharedProjectsResponse resp = new ListSharedProjectsResponse();
            do
            {
                try
                {
                    ListSharedProjectsRequest req = new ListSharedProjectsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListSharedProjectsAsync(req);
                    
                    foreach (var obj in resp.Projects)
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