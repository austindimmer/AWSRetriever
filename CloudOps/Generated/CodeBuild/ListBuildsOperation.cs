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

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeBuildConfig config = new AmazonCodeBuildConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeBuildClient client = new AmazonCodeBuildClient(creds, config);
            
            ListBuildsResponse resp = new ListBuildsResponse();
            do
            {
                try
                {
                    ListBuildsRequest req = new ListBuildsRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListBuildsAsync(req);
                    
                    foreach (var obj in resp.Ids)
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