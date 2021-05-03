using Amazon;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using Amazon.Runtime;

namespace CloudOps.IdentityManagement
{
    public class ListUsersOperation : Operation
    {
        public override string Name => "ListUsers";

        public override string Description => "Lists the IAM users that have the specified path prefix. If no path prefix is specified, the operation returns all users in the AWS account. If there are none, the operation returns an empty list.  IAM resource-listing operations return a subset of the available attributes for the resource. For example, this operation does not return tags, even though they are an attribute of the returned object. To view all of the information for a user, see GetUser.  You can paginate the results using the MaxItems and Marker parameters.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "IdentityManagement";

        public override string ServiceID => "IAM";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIdentityManagementServiceConfig config = new AmazonIdentityManagementServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIdentityManagementServiceClient client = new AmazonIdentityManagementServiceClient(creds, config);
            
            ListUsersResponse resp = new ListUsersResponse();
            do
            {
                ListUsersRequest req = new ListUsersRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = client.ListUsers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Users)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}