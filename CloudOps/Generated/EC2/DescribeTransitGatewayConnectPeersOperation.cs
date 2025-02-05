using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeTransitGatewayConnectPeersOperation : Operation
    {
        public override string Name => "DescribeTransitGatewayConnectPeers";

        public override string Description => "Describes one or more Connect peers.";
 
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
            
            DescribeTransitGatewayConnectPeersResponse resp = new DescribeTransitGatewayConnectPeersResponse();
            do
            {
                DescribeTransitGatewayConnectPeersRequest req = new DescribeTransitGatewayConnectPeersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeTransitGatewayConnectPeers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TransitGatewayConnectPeers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}