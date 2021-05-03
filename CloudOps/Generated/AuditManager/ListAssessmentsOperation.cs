using Amazon;
using Amazon.AuditManager;
using Amazon.AuditManager.Model;
using Amazon.Runtime;

namespace CloudOps.AuditManager
{
    public class ListAssessmentsOperation : Operation
    {
        public override string Name => "ListAssessments";

        public override string Description => " Returns a list of current and past assessments from AWS Audit Manager. ";
 
        public override string RequestURI => "/assessments";

        public override string Method => "GET";

        public override string ServiceName => "AuditManager";

        public override string ServiceID => "AuditManager";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAuditManagerConfig config = new AmazonAuditManagerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAuditManagerClient client = new AmazonAuditManagerClient(creds, config);
            
            ListAssessmentsResponse resp = new ListAssessmentsResponse();
            do
            {
                ListAssessmentsRequest req = new ListAssessmentsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListAssessments(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AssessmentMetadata)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}