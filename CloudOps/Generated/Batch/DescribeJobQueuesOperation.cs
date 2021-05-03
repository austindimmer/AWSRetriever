using Amazon;
using Amazon.Batch;
using Amazon.Batch.Model;
using Amazon.Runtime;

namespace CloudOps.Batch
{
    public class DescribeJobQueuesOperation : Operation
    {
        public override string Name => "DescribeJobQueues";

        public override string Description => "Describes one or more of your job queues.";
 
        public override string RequestURI => "/v1/describejobqueues";

        public override string Method => "POST";

        public override string ServiceName => "Batch";

        public override string ServiceID => "Batch";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBatchConfig config = new AmazonBatchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBatchClient client = new AmazonBatchClient(creds, config);
            
            DescribeJobQueuesResponse resp = new DescribeJobQueuesResponse();
            do
            {
                DescribeJobQueuesRequest req = new DescribeJobQueuesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeJobQueues(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.JobQueues)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}