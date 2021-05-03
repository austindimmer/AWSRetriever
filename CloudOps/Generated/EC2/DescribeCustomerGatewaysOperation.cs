using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeCustomerGatewaysOperation : Operation
    {
        public override string Name => "DescribeCustomerGateways";

        public override string Description => "Describes one or more of your VPN customer gateways. For more information, see AWS Site-to-Site VPN in the AWS Site-to-Site VPN User Guide.";
 
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
            
            DescribeCustomerGatewaysResponse resp = new DescribeCustomerGatewaysResponse();
            DescribeCustomerGatewaysRequest req = new DescribeCustomerGatewaysRequest
            {                    
                                    
            };
            resp = client.DescribeCustomerGateways(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.CustomerGateways)
            {
                AddObject(obj);
            }
            
        }
    }
}