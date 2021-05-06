using Amazon;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using Amazon.Runtime;

namespace CloudOps.IdentityManagement
{
    public class ListRolesOperation : Operation
    {
        public override string Name => "ListRoles";

        public override string Description => "Lists the IAM roles that have the specified path prefix. If there are none, the operation returns an empty list. For more information about roles, see Working with roles.  IAM resource-listing operations return a subset of the available attributes for the resource. For example, this operation does not return tags, even though they are an attribute of the returned object. To view all of the information for a role, see GetRole.  You can paginate the results using the MaxItems and Marker parameters.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "IdentityManagement";

        public override string ServiceID => "IAM";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIdentityManagementServiceConfig config = new AmazonIdentityManagementServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIdentityManagementServiceClient client = new AmazonIdentityManagementServiceClient(creds, config);
            
            ListRolesResponse resp = new ListRolesResponse();
            do
            {
                try
                {
                    ListRolesRequest req = new ListRolesRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxItems = maxItems
                                            
                    };

                    resp = await client.ListRolesAsync(req);
                    
                    foreach (var obj in resp.Roles)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}