using Amazon;
using Amazon.CodeBuild;
using Amazon.CodeBuild.Model;
using Amazon.Runtime;

namespace CloudOps.CodeBuild
{
    public class ListBuildBatchesForProjectOperation : Operation
    {
        public override string Name => "ListBuildBatchesForProject";

        public override string Description => "Retrieves the identifiers of the build batches for a specific project.";
 
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
            
            ListBuildBatchesForProjectResponse resp = new ListBuildBatchesForProjectResponse();
            do
            {
                try
                {
                    ListBuildBatchesForProjectRequest req = new ListBuildBatchesForProjectRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListBuildBatchesForProjectAsync(req);
                    
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