using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeAccountAttributesOperation : Operation
    {
        public override string Name => "DescribeAccountAttributes";

        public override string Description => "Describes attributes of your AWS account. The following are the supported account attributes:    supported-platforms: Indicates whether your account can launch instances into EC2-Classic and EC2-VPC, or only into EC2-VPC.    default-vpc: The ID of the default VPC for your account, or none.    max-instances: This attribute is no longer supported. The returned value does not reflect your actual vCPU limit for running On-Demand Instances. For more information, see On-Demand Instance Limits in the Amazon Elastic Compute Cloud User Guide.    vpc-max-security-groups-per-interface: The maximum number of security groups that you can assign to a network interface.    max-elastic-ips: The maximum number of Elastic IP addresses that you can allocate for use with EC2-Classic.     vpc-max-elastic-ips: The maximum number of Elastic IP addresses that you can allocate for use with EC2-VPC.  ";
 
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
            
            DescribeAccountAttributesResponse resp = new DescribeAccountAttributesResponse();
            DescribeAccountAttributesRequest req = new DescribeAccountAttributesRequest
            {                    
                                    
            };
            resp = await client.DescribeAccountAttributesAsync(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.AccountAttributes)
            {
                AddObject(obj);
            }
            
        }
    }
}