using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeLocalGatewayRouteTableVpcAssociationsOperation : Operation
    {
        public override string Name => "DescribeLocalGatewayRouteTableVpcAssociations";

        public override string Description => "Describes the specified associations between VPCs and local gateway route tables.";
 
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
            
            DescribeLocalGatewayRouteTableVpcAssociationsResponse resp = new DescribeLocalGatewayRouteTableVpcAssociationsResponse();
            do
            {
                DescribeLocalGatewayRouteTableVpcAssociationsRequest req = new DescribeLocalGatewayRouteTableVpcAssociationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeLocalGatewayRouteTableVpcAssociations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.LocalGatewayRouteTableVpcAssociations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}