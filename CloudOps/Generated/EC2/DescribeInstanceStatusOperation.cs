using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeInstanceStatusOperation : Operation
    {
        public override string Name => "DescribeInstanceStatus";

        public override string Description => "Describes the status of the specified instances or all of your instances. By default, only running instances are described, unless you specifically indicate to return the status of all instances. Instance status includes the following components:    Status checks - Amazon EC2 performs status checks on running EC2 instances to identify hardware and software issues. For more information, see Status checks for your instances and Troubleshooting instances with failed status checks in the Amazon EC2 User Guide.    Scheduled events - Amazon EC2 can schedule events (such as reboot, stop, or terminate) for your instances related to hardware issues, software updates, or system maintenance. For more information, see Scheduled events for your instances in the Amazon EC2 User Guide.    Instance state - You can manage your instances from the moment you launch them through their termination. For more information, see Instance lifecycle in the Amazon EC2 User Guide.  ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            DescribeInstanceStatusResponse resp = new DescribeInstanceStatusResponse();
            do
            {
                try
                {
                    DescribeInstanceStatusRequest req = new DescribeInstanceStatusRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeInstanceStatusAsync(req);
                    
                    foreach (var obj in resp.InstanceStatuses)
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