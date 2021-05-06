using Amazon;
using Amazon.CodeCommit;
using Amazon.CodeCommit.Model;
using Amazon.Runtime;

namespace CloudOps.CodeCommit
{
    public class ListRepositoriesForApprovalRuleTemplateOperation : Operation
    {
        public override string Name => "ListRepositoriesForApprovalRuleTemplate";

        public override string Description => "Lists all repositories associated with the specified approval rule template.";
 
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
            
            ListRepositoriesForApprovalRuleTemplateResponse resp = new ListRepositoriesForApprovalRuleTemplateResponse();
            do
            {
                ListRepositoriesForApprovalRuleTemplateRequest req = new ListRepositoriesForApprovalRuleTemplateRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListRepositoriesForApprovalRuleTemplateAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.RepositoryNames)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}