using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeVpcEndpointsOperation : Operation
    {
        public override string Name => "DescribeVpcEndpoints";

        public override string Description => "Describes one or more of your VPC endpoints.";
 
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
            
            DescribeVpcEndpointsResponse resp = new DescribeVpcEndpointsResponse();
            do
            {
                DescribeVpcEndpointsRequest req = new DescribeVpcEndpointsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeVpcEndpoints(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.VpcEndpoints)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}