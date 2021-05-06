using Amazon;
using Amazon.CodeCommit;
using Amazon.CodeCommit.Model;
using Amazon.Runtime;

namespace CloudOps.CodeCommit
{
    public class ListRepositoriesOperation : Operation
    {
        public override string Name => "ListRepositories";

        public override string Description => "Gets information about one or more repositories.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeCommit";

        public override string ServiceID => "CodeCommit";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeCommitConfig config = new AmazonCodeCommitConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeCommitClient client = new AmazonCodeCommitClient(creds, config);
            
            ListRepositoriesResponse resp = new ListRepositoriesResponse();
            do
            {
                try
                {
                    ListRepositoriesRequest req = new ListRepositoriesRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListRepositoriesAsync(req);
                    
                    foreach (var obj in resp.Repositories)
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