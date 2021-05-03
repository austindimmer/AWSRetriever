using Amazon;
using Amazon.LookoutMetrics;
using Amazon.LookoutMetrics.Model;
using Amazon.Runtime;

namespace CloudOps.LookoutMetrics
{
    public class ListAlertsOperation : Operation
    {
        public override string Name => "ListAlerts";

        public override string Description => "Lists the alerts attached to a detector.";
 
        public override string RequestURI => "/ListAlerts";

        public override string Method => "POST";

        public override string ServiceName => "LookoutMetrics";

        public override string ServiceID => "LookoutMetrics";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLookoutMetricsConfig config = new AmazonLookoutMetricsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLookoutMetricsClient client = new AmazonLookoutMetricsClient(creds, config);
            
            ListAlertsResponse resp = new ListAlertsResponse();
            do
            {
                ListAlertsRequest req = new ListAlertsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListAlerts(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AlertSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}