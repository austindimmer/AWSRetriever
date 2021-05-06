using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeCarrierGatewaysOperation : Operation
    {
        public override string Name => "DescribeCarrierGateways";

        public override string Description => "Describes one or more of your carrier gateways.";
 
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
            
            DescribeCarrierGatewaysResponse resp = new DescribeCarrierGatewaysResponse();
            do
            {
                try
                {
                    DescribeCarrierGatewaysRequest req = new DescribeCarrierGatewaysRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeCarrierGatewaysAsync(req);
                    
                    foreach (var obj in resp.CarrierGateways)
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