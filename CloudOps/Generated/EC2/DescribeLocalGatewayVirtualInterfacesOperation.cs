using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeLocalGatewayVirtualInterfacesOperation : Operation
    {
        public override string Name => "DescribeLocalGatewayVirtualInterfaces";

        public override string Description => "Describes the specified local gateway virtual interfaces.";
 
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
            
            DescribeLocalGatewayVirtualInterfacesResponse resp = new DescribeLocalGatewayVirtualInterfacesResponse();
            do
            {
                DescribeLocalGatewayVirtualInterfacesRequest req = new DescribeLocalGatewayVirtualInterfacesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeLocalGatewayVirtualInterfacesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.LocalGatewayVirtualInterfaces)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}