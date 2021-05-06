using Amazon;
using Amazon.ElasticLoadBalancingV2;
using Amazon.ElasticLoadBalancingV2.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticLoadBalancingV2
{
    public class DescribeLoadBalancersOperation : Operation
    {
        public override string Name => "DescribeLoadBalancers";

        public override string Description => "Describes the specified load balancers or all of your load balancers.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ElasticLoadBalancingV2";

        public override string ServiceID => "Elastic Load Balancing v2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticLoadBalancingV2Config config = new AmazonElasticLoadBalancingV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticLoadBalancingV2Client client = new AmazonElasticLoadBalancingV2Client(creds, config);
            
            DescribeLoadBalancersResponse resp = new DescribeLoadBalancersResponse();
            do
            {
                DescribeLoadBalancersRequest req = new DescribeLoadBalancersRequest
                {
                    Marker = resp.NextMarker
                                        
                };

                resp = await client.DescribeLoadBalancersAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.LoadBalancers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}