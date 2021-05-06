using Amazon;
using Amazon.OpsWorks;
using Amazon.OpsWorks.Model;
using Amazon.Runtime;

namespace CloudOps.OpsWorks
{
    public class DescribeLayersOperation : Operation
    {
        public override string Name => "DescribeLayers";

        public override string Description => "Requests a description of one or more layers in a specified stack.  This call accepts only one resource-identifying parameter.   Required Permissions: To use this action, an IAM user must have a Show, Deploy, or Manage permissions level for the stack, or an attached policy that explicitly grants permissions. For more information about user permissions, see Managing User Permissions.";
 
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
            
            DescribeLayersResponse resp = new DescribeLayersResponse();
            DescribeLayersRequest req = new DescribeLayersRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribeLayersAsync(req);
                
                foreach (var obj in resp.Layers)
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
    }
}