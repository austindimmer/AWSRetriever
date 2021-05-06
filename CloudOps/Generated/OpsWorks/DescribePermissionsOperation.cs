using Amazon;
using Amazon.OpsWorks;
using Amazon.OpsWorks.Model;
using Amazon.Runtime;

namespace CloudOps.OpsWorks
{
    public class DescribePermissionsOperation : Operation
    {
        public override string Name => "DescribePermissions";

        public override string Description => "Describes the permissions for a specified stack.  Required Permissions: To use this action, an IAM user must have a Manage permissions level for the stack, or an attached policy that explicitly grants permissions. For more information on user permissions, see Managing User Permissions.";
 
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
            
            DescribePermissionsResponse resp = new DescribePermissionsResponse();
            DescribePermissionsRequest req = new DescribePermissionsRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribePermissionsAsync(req);
                
                foreach (var obj in resp.Permissions)
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