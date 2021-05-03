using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeLocalGatewayRouteTableVirtualInterfaceGroupAssociationsOperation : Operation
    {
        public override string Name => "DescribeLocalGatewayRouteTableVirtualInterfaceGroupAssociations";

        public override string Description => "Describes the associations between virtual interface groups and local gateway route tables.";
 
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
            
            DescribeLocalGatewayRouteTableVirtualInterfaceGroupAssociationsResponse resp = new DescribeLocalGatewayRouteTableVirtualInterfaceGroupAssociationsResponse();
            do
            {
                DescribeLocalGatewayRouteTableVirtualInterfaceGroupAssociationsRequest req = new DescribeLocalGatewayRouteTableVirtualInterfaceGroupAssociationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeLocalGatewayRouteTableVirtualInterfaceGroupAssociations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.LocalGatewayRouteTableVirtualInterfaceGroupAssociations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}