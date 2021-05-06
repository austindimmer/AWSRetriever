using Amazon;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Amazon.Runtime;

namespace CloudOps.CloudWatch
{
    public class DescribeAlarmsForMetricOperation : Operation
    {
        public override string Name => "DescribeAlarmsForMetric";

        public override string Description => "Retrieves the alarms for the specified metric. To filter the results, specify a statistic, period, or unit. This operation retrieves only standard alarms that are based on the specified metric. It does not return alarms based on math expressions that use the specified metric, or composite alarms that use the specified metric.";
 
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
            
            DescribeAlarmsForMetricResponse resp = new DescribeAlarmsForMetricResponse();
            DescribeAlarmsForMetricRequest req = new DescribeAlarmsForMetricRequest
            {                    
                                    
            };
            resp = await client.DescribeAlarmsForMetricAsync(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.MetricAlarms)
            {
                AddObject(obj);
            }
            
        }
    }
}