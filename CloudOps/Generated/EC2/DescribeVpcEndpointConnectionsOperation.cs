using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeVpcEndpointConnectionsOperation : Operation
    {
        public override string Name => "DescribeVpcEndpointConnections";

        public override string Description => "Describes the VPC endpoint connections to your VPC endpoint services, including any endpoints that are pending your acceptance.";
 
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
            
            DescribeVpcEndpointConnectionsResponse resp = new DescribeVpcEndpointConnectionsResponse();
            do
            {
                DescribeVpcEndpointConnectionsRequest req = new DescribeVpcEndpointConnectionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeVpcEndpointConnectionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.VpcEndpointConnections)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}