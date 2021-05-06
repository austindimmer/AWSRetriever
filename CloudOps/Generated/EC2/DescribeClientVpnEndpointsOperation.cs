using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeClientVpnEndpointsOperation : Operation
    {
        public override string Name => "DescribeClientVpnEndpoints";

        public override string Description => "Describes one or more Client VPN endpoints in the account.";
 
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
            
            DescribeClientVpnEndpointsResponse resp = new DescribeClientVpnEndpointsResponse();
            do
            {
                try
                {
                    DescribeClientVpnEndpointsRequest req = new DescribeClientVpnEndpointsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeClientVpnEndpointsAsync(req);
                    
                    foreach (var obj in resp.ClientVpnEndpoints)
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