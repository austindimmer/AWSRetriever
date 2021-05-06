using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeVpcPeeringConnectionsOperation : Operation
    {
        public override string Name => "DescribeVpcPeeringConnections";

        public override string Description => "Describes one or more of your VPC peering connections.";
 
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
            
            DescribeVpcPeeringConnectionsResponse resp = new DescribeVpcPeeringConnectionsResponse();
            do
            {
                try
                {
                    DescribeVpcPeeringConnectionsRequest req = new DescribeVpcPeeringConnectionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeVpcPeeringConnectionsAsync(req);
                    
                    foreach (var obj in resp.VpcPeeringConnections)
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