using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeReservedInstancesOperation : Operation
    {
        public override string Name => "DescribeReservedInstances";

        public override string Description => "Describes one or more of the Reserved Instances that you purchased. For more information about Reserved Instances, see Reserved Instances in the Amazon EC2 User Guide.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            DescribeReservedInstancesResponse resp = new DescribeReservedInstancesResponse();
            DescribeReservedInstancesRequest req = new DescribeReservedInstancesRequest
            {                    
                                    
            };
            resp = client.DescribeReservedInstances(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.ReservedInstances)
            {
                AddObject(obj);
            }
            
        }
    }
}