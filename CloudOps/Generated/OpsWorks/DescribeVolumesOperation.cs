using Amazon;
using Amazon.OpsWorks;
using Amazon.OpsWorks.Model;
using Amazon.Runtime;

namespace CloudOps.OpsWorks
{
    public class DescribeVolumesOperation : Operation
    {
        public override string Name => "DescribeVolumes";

        public override string Description => "Describes an instance&#39;s Amazon EBS volumes.  This call accepts only one resource-identifying parameter.   Required Permissions: To use this action, an IAM user must have a Show, Deploy, or Manage permissions level for the stack, or an attached policy that explicitly grants permissions. For more information about user permissions, see Managing User Permissions.";
 
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
            
            DescribeVolumesResponse resp = new DescribeVolumesResponse();
            DescribeVolumesRequest req = new DescribeVolumesRequest
            {                    
                                    
            };
            resp = await client.DescribeVolumesAsync(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.Volumes)
            {
                AddObject(obj);
            }
            
        }
    }
}