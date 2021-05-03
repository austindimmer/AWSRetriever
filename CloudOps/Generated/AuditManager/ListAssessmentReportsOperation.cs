using Amazon;
using Amazon.AuditManager;
using Amazon.AuditManager.Model;
using Amazon.Runtime;

namespace CloudOps.AuditManager
{
    public class ListAssessmentReportsOperation : Operation
    {
        public override string Name => "ListAssessmentReports";

        public override string Description => " Returns a list of assessment reports created in AWS Audit Manager. ";
 
        public override string RequestURI => "/assessmentReports";

        public override string Method => "GET";

        public override string ServiceName => "AuditManager";

        public override string ServiceID => "AuditManager";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAuditManagerConfig config = new AmazonAuditManagerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAuditManagerClient client = new AmazonAuditManagerClient(creds, config);
            
            ListAssessmentReportsResponse resp = new ListAssessmentReportsResponse();
            do
            {
                ListAssessmentReportsRequest req = new ListAssessmentReportsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListAssessmentReports(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AssessmentReports)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}