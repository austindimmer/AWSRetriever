using Amazon;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Amazon.Runtime;

namespace CloudOps.CloudWatch
{
    public class ListMetricsOperation : Operation
    {
        public override string Name => "ListMetrics";

        public override string Description => "List the specified metrics. You can use the returned metrics with GetMetricData or GetMetricStatistics to obtain statistical data. Up to 500 results are returned for any one call. To retrieve additional results, use the returned token with subsequent calls. After you create a metric, allow up to 15 minutes before the metric appears. You can see statistics about the metric sooner by using GetMetricData or GetMetricStatistics.  ListMetrics doesn&#39;t return information about metrics if those metrics haven&#39;t reported data in the past two weeks. To retrieve those metrics, use GetMetricData or GetMetricStatistics.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudWatch";

        public override string ServiceID => "CloudWatch";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudWatchConfig config = new AmazonCloudWatchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudWatchClient client = new AmazonCloudWatchClient(creds, config);
            
            ListMetricsResponse resp = new ListMetricsResponse();
            do
            {
                try
                {
                    ListMetricsRequest req = new ListMetricsRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListMetricsAsync(req);
                    
                    foreach (var obj in resp.Metrics)
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