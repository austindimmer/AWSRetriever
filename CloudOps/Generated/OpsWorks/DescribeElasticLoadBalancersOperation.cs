using Amazon;
using Amazon.OpsWorks;
using Amazon.OpsWorks.Model;
using Amazon.Runtime;

namespace CloudOps.OpsWorks
{
    public class DescribeElasticLoadBalancersOperation : Operation
    {
        public override string Name => "DescribeElasticLoadBalancers";

        public override string Description => "Describes a stack&#39;s Elastic Load Balancing instances.  This call accepts only one resource-identifying parameter.   Required Permissions: To use this action, an IAM user must have a Show, Deploy, or Manage permissions level for the stack, or an attached policy that explicitly grants permissions. For more information about user permissions, see Managing User Permissions.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "OpsWorks";

        public override string ServiceID => "OpsWorks";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonOpsWorksConfig config = new AmazonOpsWorksConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonOpsWorksClient client = new AmazonOpsWorksClient(creds, config);
            
            DescribeElasticLoadBalancersResponse resp = new DescribeElasticLoadBalancersResponse();
            DescribeElasticLoadBalancersRequest req = new DescribeElasticLoadBalancersRequest
            {                    
                                    
            };
            resp = await client.DescribeElasticLoadBalancersAsync(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.ElasticLoadBalancers)
            {
                AddObject(obj);
            }
            
        }
    }
}