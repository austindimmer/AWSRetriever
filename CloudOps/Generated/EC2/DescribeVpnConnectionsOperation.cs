using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeVpnConnectionsOperation : Operation
    {
        public override string Name => "DescribeVpnConnections";

        public override string Description => "Describes one or more of your VPN connections. For more information, see AWS Site-to-Site VPN in the AWS Site-to-Site VPN User Guide.";
 
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
            
            DescribeVpnConnectionsResponse resp = new DescribeVpnConnectionsResponse();
            DescribeVpnConnectionsRequest req = new DescribeVpnConnectionsRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribeVpnConnectionsAsync(req);
                
                foreach (var obj in resp.VpnConnections)
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
    }
}