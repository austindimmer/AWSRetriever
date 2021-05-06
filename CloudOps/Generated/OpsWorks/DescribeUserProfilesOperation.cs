using Amazon;
using Amazon.OpsWorks;
using Amazon.OpsWorks.Model;
using Amazon.Runtime;

namespace CloudOps.OpsWorks
{
    public class DescribeUserProfilesOperation : Operation
    {
        public override string Name => "DescribeUserProfiles";

        public override string Description => "Describe specified users.  Required Permissions: To use this action, an IAM user must have an attached policy that explicitly grants permissions. For more information about user permissions, see Managing User Permissions.";
 
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
            
            DescribeUserProfilesResponse resp = new DescribeUserProfilesResponse();
            DescribeUserProfilesRequest req = new DescribeUserProfilesRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribeUserProfilesAsync(req);
                
                foreach (var obj in resp.UserProfiles)
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