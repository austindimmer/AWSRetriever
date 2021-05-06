using Amazon;
using Amazon.LookoutMetrics;
using Amazon.LookoutMetrics.Model;
using Amazon.Runtime;

namespace CloudOps.LookoutMetrics
{
    public class ListAnomalyDetectorsOperation : Operation
    {
        public override string Name => "ListAnomalyDetectors";

        public override string Description => "Lists the detectors in the current AWS Region.";
 
        public override string RequestURI => "/ListAnomalyDetectors";

        public override string Method => "POST";

        public override string ServiceName => "LookoutMetrics";

        public override string ServiceID => "LookoutMetrics";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLookoutMetricsConfig config = new AmazonLookoutMetricsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLookoutMetricsClient client = new AmazonLookoutMetricsClient(creds, config);
            
            ListAnomalyDetectorsResponse resp = new ListAnomalyDetectorsResponse();
            do
            {
                ListAnomalyDetectorsRequest req = new ListAnomalyDetectorsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListAnomalyDetectorsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AnomalyDetectorSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}