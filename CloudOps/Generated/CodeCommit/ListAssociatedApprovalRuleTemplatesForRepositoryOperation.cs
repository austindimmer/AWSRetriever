using Amazon;
using Amazon.CodeCommit;
using Amazon.CodeCommit.Model;
using Amazon.Runtime;

namespace CloudOps.CodeCommit
{
    public class ListAssociatedApprovalRuleTemplatesForRepositoryOperation : Operation
    {
        public override string Name => "ListAssociatedApprovalRuleTemplatesForRepository";

        public override string Description => "Lists all approval rule templates that are associated with a specified repository.";
 
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
            
            ListAssociatedApprovalRuleTemplatesForRepositoryResponse resp = new ListAssociatedApprovalRuleTemplatesForRepositoryResponse();
            do
            {
                ListAssociatedApprovalRuleTemplatesForRepositoryRequest req = new ListAssociatedApprovalRuleTemplatesForRepositoryRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListAssociatedApprovalRuleTemplatesForRepositoryAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ApprovalRuleTemplateNames)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}