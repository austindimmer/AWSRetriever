using Amazon;
using Amazon.Batch;
using Amazon.Batch.Model;
using Amazon.Runtime;

namespace CloudOps.Batch
{
    public class DescribeComputeEnvironmentsOperation : Operation
    {
        public override string Name => "DescribeComputeEnvironments";

        public override string Description => "Describes one or more of your compute environments. If you&#39;re using an unmanaged compute environment, you can use the DescribeComputeEnvironment operation to determine the ecsClusterArn that you should launch your Amazon ECS container instances into.";
 
        public override string RequestURI => "/v1/describecomputeenvironments";

        public override string Method => "POST";

        public override string ServiceName => "Batch";

        public override string ServiceID => "Batch";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBatchConfig config = new AmazonBatchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBatchClient client = new AmazonBatchClient(creds, config);
            
            DescribeComputeEnvironmentsResponse resp = new DescribeComputeEnvironmentsResponse();
            do
            {
                DescribeComputeEnvironmentsRequest req = new DescribeComputeEnvironmentsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeComputeEnvironmentsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ComputeEnvironments)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}