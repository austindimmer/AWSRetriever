using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeCoipPoolsOperation : Operation
    {
        public override string Name => "DescribeCoipPools";

        public override string Description => "Describes the specified customer-owned address pools or all of your customer-owned address pools.";
 
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
            
            DescribeCoipPoolsResponse resp = new DescribeCoipPoolsResponse();
            do
            {
                DescribeCoipPoolsRequest req = new DescribeCoipPoolsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeCoipPools(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CoipPools)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}