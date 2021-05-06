using Amazon;
using Amazon.CodeArtifact;
using Amazon.CodeArtifact.Model;
using Amazon.Runtime;

namespace CloudOps.CodeArtifact
{
    public class ListRepositoriesOperation : Operation
    {
        public override string Name => "ListRepositories";

        public override string Description => " Returns a list of  RepositorySummary  objects. Each RepositorySummary contains information about a repository in the specified AWS account and that matches the input parameters. ";
 
        public override string RequestURI => "/v1/repositories";

        public override string Method => "POST";

        public override string ServiceName => "CodeArtifact";

        public override string ServiceID => "codeartifact";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeArtifactConfig config = new AmazonCodeArtifactConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeArtifactClient client = new AmazonCodeArtifactClient(creds, config);
            
            ListRepositoriesResponse resp = new ListRepositoriesResponse();
            do
            {
                ListRepositoriesRequest req = new ListRepositoriesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListRepositoriesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Repositories)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}