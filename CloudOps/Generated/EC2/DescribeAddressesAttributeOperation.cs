using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeAddressesAttributeOperation : Operation
    {
        public override string Name => "DescribeAddressesAttribute";

        public override string Description => "Describes the attributes of the specified Elastic IP addresses. For requirements, see Using reverse DNS for email applications.";
 
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
            
            DescribeAddressesAttributeResponse resp = new DescribeAddressesAttributeResponse();
            do
            {
                DescribeAddressesAttributeRequest req = new DescribeAddressesAttributeRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeAddressesAttribute(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Addresses)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}