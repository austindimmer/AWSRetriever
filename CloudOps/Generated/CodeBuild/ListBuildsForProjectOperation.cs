using Amazon;
using Amazon.CodeBuild;
using Amazon.CodeBuild.Model;
using Amazon.Runtime;

namespace CloudOps.CodeBuild
{
    public class ListBuildsForProjectOperation : Operation
    {
        public override string Name => "ListBuildsForProject";

        public override string Description => "Gets a list of build identifiers for the specified build project, with each build identifier representing a single build.";
 
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
            
            ListBuildsForProjectResponse resp = new ListBuildsForProjectResponse();
            do
            {
                ListBuildsForProjectRequest req = new ListBuildsForProjectRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = await client.ListBuildsForProjectAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Ids)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}