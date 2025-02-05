using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListMonitoringSchedulesOperation : Operation
    {
        public override string Name => "ListMonitoringSchedules";

        public override string Description => "Returns list of all monitoring schedules.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SageMaker";

        public override string ServiceID => "SageMaker";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSageMakerConfig config = new AmazonSageMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSageMakerClient client = new AmazonSageMakerClient(creds, config);
            
            ListMonitoringSchedulesResponse resp = new ListMonitoringSchedulesResponse();
            do
            {
                ListMonitoringSchedulesRequest req = new ListMonitoringSchedulesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListMonitoringSchedules(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.MonitoringScheduleSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}