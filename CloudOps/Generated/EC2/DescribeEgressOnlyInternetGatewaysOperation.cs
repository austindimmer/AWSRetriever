using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeEgressOnlyInternetGatewaysOperation : Operation
    {
        public override string Name => "DescribeEgressOnlyInternetGateways";

        public override string Description => "Describes one or more of your egress-only internet gateways.";
 
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
            
            DescribeEgressOnlyInternetGatewaysResponse resp = new DescribeEgressOnlyInternetGatewaysResponse();
            do
            {
                DescribeEgressOnlyInternetGatewaysRequest req = new DescribeEgressOnlyInternetGatewaysRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeEgressOnlyInternetGatewaysAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.EgressOnlyInternetGateways)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}