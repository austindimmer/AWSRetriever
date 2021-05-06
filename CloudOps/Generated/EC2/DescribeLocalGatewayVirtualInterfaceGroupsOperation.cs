using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeLocalGatewayVirtualInterfaceGroupsOperation : Operation
    {
        public override string Name => "DescribeLocalGatewayVirtualInterfaceGroups";

        public override string Description => "Describes the specified local gateway virtual interface groups.";
 
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
            
            DescribeLocalGatewayVirtualInterfaceGroupsResponse resp = new DescribeLocalGatewayVirtualInterfaceGroupsResponse();
            do
            {
                DescribeLocalGatewayVirtualInterfaceGroupsRequest req = new DescribeLocalGatewayVirtualInterfaceGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeLocalGatewayVirtualInterfaceGroupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.LocalGatewayVirtualInterfaceGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}