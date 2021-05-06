using Amazon;
using Amazon.AutoScaling;
using Amazon.AutoScaling.Model;
using Amazon.Runtime;

namespace CloudOps.AutoScaling
{
    public class DescribeAutoScalingInstancesOperation : Operation
    {
        public override string Name => "DescribeAutoScalingInstances";

        public override string Description => "Describes one or more Auto Scaling instances.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "AutoScaling";

        public override string ServiceID => "Auto Scaling";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAutoScalingConfig config = new AmazonAutoScalingConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAutoScalingClient client = new AmazonAutoScalingClient(creds, config);
            
            DescribeAutoScalingInstancesResponse resp = new DescribeAutoScalingInstancesResponse();
            do
            {
                try
                {
                    DescribeAutoScalingInstancesRequest req = new DescribeAutoScalingInstancesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeAutoScalingInstancesAsync(req);
                    
                    foreach (var obj in resp.AutoScalingInstances)
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