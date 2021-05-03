using Amazon;
using Amazon.OpsWorks;
using Amazon.OpsWorks.Model;
using Amazon.Runtime;

namespace CloudOps.OpsWorks
{
    public class DescribeServiceErrorsOperation : Operation
    {
        public override string Name => "DescribeServiceErrors";

        public override string Description => "Describes AWS OpsWorks Stacks service errors.  Required Permissions: To use this action, an IAM user must have a Show, Deploy, or Manage permissions level for the stack, or an attached policy that explicitly grants permissions. For more information about user permissions, see Managing User Permissions. This call accepts only one resource-identifying parameter.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "OpsWorks";

        public override string ServiceID => "OpsWorks";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonOpsWorksConfig config = new AmazonOpsWorksConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonOpsWorksClient client = new AmazonOpsWorksClient(creds, config);
            
            DescribeServiceErrorsResponse resp = new DescribeServiceErrorsResponse();
            DescribeServiceErrorsRequest req = new DescribeServiceErrorsRequest
            {                    
                                    
            };
            resp = client.DescribeServiceErrors(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.ServiceErrors)
            {
                AddObject(obj);
            }
            
        }
    }
}