using Amazon;
using Amazon.CodeBuild;
using Amazon.CodeBuild.Model;
using Amazon.Runtime;

namespace CloudOps.CodeBuild
{
    public class ListBuildsOperation : Operation
    {
        public override string Name => "ListBuilds";

        public override string Description => "Gets a list of build IDs, with each build ID representing a single build.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeBuild";

        public override string ServiceID => "CodeBuild";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeBuildConfig config = new AmazonCodeBuildConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeBuildClient client = new AmazonCodeBuildClient(creds, config);
            
            ListBuildsResponse resp = new ListBuildsResponse();
            do
            {
                ListBuildsRequest req = new ListBuildsRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = client.ListBuilds(req);
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