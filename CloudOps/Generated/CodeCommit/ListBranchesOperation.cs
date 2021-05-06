using Amazon;
using Amazon.CodeCommit;
using Amazon.CodeCommit.Model;
using Amazon.Runtime;

namespace CloudOps.CodeCommit
{
    public class ListBranchesOperation : Operation
    {
        public override string Name => "ListBranches";

        public override string Description => "Gets information about one or more branches in a repository.";
 
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
            
            ListBranchesResponse resp = new ListBranchesResponse();
            do
            {
                try
                {
                    ListBranchesRequest req = new ListBranchesRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListBranchesAsync(req);
                    
                    foreach (var obj in resp.Branches)
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