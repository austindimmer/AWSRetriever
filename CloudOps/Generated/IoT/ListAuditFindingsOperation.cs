using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListAuditFindingsOperation : Operation
    {
        public override string Name => "ListAuditFindings";

        public override string Description => "Lists the findings (results) of a Device Defender audit or of the audits performed during a specified time period. (Findings are retained for 90 days.)";
 
        public override string RequestURI => "/audit/findings";

        public override string Method => "POST";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListAuditFindingsResponse resp = new ListAuditFindingsResponse();
            do
            {
                ListAuditFindingsRequest req = new ListAuditFindingsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListAuditFindings(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Findings)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}