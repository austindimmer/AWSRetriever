using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeLocalGatewaysOperation : Operation
    {
        public override string Name => "DescribeLocalGateways";

        public override string Description => "Describes one or more local gateways. By default, all local gateways are described. Alternatively, you can filter the results.";
 
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
            
            DescribeLocalGatewaysResponse resp = new DescribeLocalGatewaysResponse();
            do
            {
                try
                {
                    DescribeLocalGatewaysRequest req = new DescribeLocalGatewaysRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeLocalGatewaysAsync(req);
                    
                    foreach (var obj in resp.LocalGateways)
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