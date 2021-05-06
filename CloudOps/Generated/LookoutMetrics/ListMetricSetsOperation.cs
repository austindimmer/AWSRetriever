using Amazon;
using Amazon.LookoutMetrics;
using Amazon.LookoutMetrics.Model;
using Amazon.Runtime;

namespace CloudOps.LookoutMetrics
{
    public class ListMetricSetsOperation : Operation
    {
        public override string Name => "ListMetricSets";

        public override string Description => "Lists the datasets in the current AWS Region.";
 
        public override string RequestURI => "/ListMetricSets";

        public override string Method => "POST";

        public override string ServiceName => "LookoutMetrics";

        public override string ServiceID => "LookoutMetrics";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLookoutMetricsConfig config = new AmazonLookoutMetricsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLookoutMetricsClient client = new AmazonLookoutMetricsClient(creds, config);
            
            ListMetricSetsResponse resp = new ListMetricSetsResponse();
            do
            {
                try
                {
                    ListMetricSetsRequest req = new ListMetricSetsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListMetricSetsAsync(req);
                    
                    foreach (var obj in resp.MetricSetSummaryList)
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