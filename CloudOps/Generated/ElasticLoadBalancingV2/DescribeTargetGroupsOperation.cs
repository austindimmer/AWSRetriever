using Amazon;
using Amazon.ElasticLoadBalancingV2;
using Amazon.ElasticLoadBalancingV2.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticLoadBalancingV2
{
    public class DescribeTargetGroupsOperation : Operation
    {
        public override string Name => "DescribeTargetGroups";

        public override string Description => "Describes the specified target groups or all of your target groups. By default, all target groups are described. Alternatively, you can specify one of the following to filter the results: the ARN of the load balancer, the names of one or more target groups, or the ARNs of one or more target groups.";
 
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
            
            DescribeTargetGroupsResponse resp = new DescribeTargetGroupsResponse();
            do
            {
                DescribeTargetGroupsRequest req = new DescribeTargetGroupsRequest
                {
                    Marker = resp.NextMarker
                                        
                };

                resp = await client.DescribeTargetGroupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TargetGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}