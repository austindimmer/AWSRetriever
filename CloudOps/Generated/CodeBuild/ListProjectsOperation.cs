using Amazon;
using Amazon.CodeBuild;
using Amazon.CodeBuild.Model;
using Amazon.Runtime;

namespace CloudOps.CodeBuild
{
    public class ListProjectsOperation : Operation
    {
        public override string Name => "ListProjects";

        public override string Description => "Gets a list of build project names, with each build project name representing a single build project.";
 
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
            
            ListProjectsResponse resp = new ListProjectsResponse();
            do
            {
                try
                {
                    ListProjectsRequest req = new ListProjectsRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListProjectsAsync(req);
                    
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