using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribePublicIpv4PoolsOperation : Operation
    {
        public override string Name => "DescribePublicIpv4Pools";

        public override string Description => "Describes the specified IPv4 address pools.";
 
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
            
            DescribePublicIpv4PoolsResponse resp = new DescribePublicIpv4PoolsResponse();
            do
            {
                try
                {
                    DescribePublicIpv4PoolsRequest req = new DescribePublicIpv4PoolsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribePublicIpv4PoolsAsync(req);
                    
                    foreach (var obj in resp.PublicIpv4Pools)
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