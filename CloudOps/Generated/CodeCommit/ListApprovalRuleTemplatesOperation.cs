using Amazon;
using Amazon.CodeCommit;
using Amazon.CodeCommit.Model;
using Amazon.Runtime;

namespace CloudOps.CodeCommit
{
    public class ListApprovalRuleTemplatesOperation : Operation
    {
        public override string Name => "ListApprovalRuleTemplates";

        public override string Description => "Lists all approval rule templates in the specified AWS Region in your AWS account. If an AWS Region is not specified, the AWS Region where you are signed in is used.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeCommit";

        public override string ServiceID => "CodeCommit";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeCommitConfig config = new AmazonCodeCommitConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeCommitClient client = new AmazonCodeCommitClient(creds, config);
            
            ListApprovalRuleTemplatesResponse resp = new ListApprovalRuleTemplatesResponse();
            do
            {
                ListApprovalRuleTemplatesRequest req = new ListApprovalRuleTemplatesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListApprovalRuleTemplates(req);
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