using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeTransitGatewayMulticastDomainsOperation : Operation
    {
        public override string Name => "DescribeTransitGatewayMulticastDomains";

        public override string Description => "Describes one or more transit gateway multicast domains.";
 
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
            
            DescribeTransitGatewayMulticastDomainsResponse resp = new DescribeTransitGatewayMulticastDomainsResponse();
            do
            {
                DescribeTransitGatewayMulticastDomainsRequest req = new DescribeTransitGatewayMulticastDomainsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeTransitGatewayMulticastDomainsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TransitGatewayMulticastDomains)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}