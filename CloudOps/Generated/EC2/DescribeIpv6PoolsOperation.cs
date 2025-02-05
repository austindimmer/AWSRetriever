using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeIpv6PoolsOperation : Operation
    {
        public override string Name => "DescribeIpv6Pools";

        public override string Description => "Describes your IPv6 address pools.";
 
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
            
            DescribeIpv6PoolsResponse resp = new DescribeIpv6PoolsResponse();
            do
            {
                DescribeIpv6PoolsRequest req = new DescribeIpv6PoolsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeIpv6Pools(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Ipv6Pools)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}