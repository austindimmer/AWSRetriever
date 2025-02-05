using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleSystemsManagement
{
    public class ListComplianceSummariesOperation : Operation
    {
        public override string Name => "ListComplianceSummaries";

        public override string Description => "Returns a summary count of compliant and non-compliant resources for a compliance type. For example, this call can return State Manager associations, patches, or custom compliance types according to the filter criteria that you specify.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SimpleSystemsManagement";

        public override string ServiceID => "SSM";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleSystemsManagementConfig config = new AmazonSimpleSystemsManagementConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleSystemsManagementClient client = new AmazonSimpleSystemsManagementClient(creds, config);
            
            ListComplianceSummariesResponse resp = new ListComplianceSummariesResponse();
            do
            {
                ListComplianceSummariesRequest req = new ListComplianceSummariesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListComplianceSummaries(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ComplianceSummaryItems)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}