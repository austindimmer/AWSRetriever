using Amazon;
using Amazon.LakeFormation;
using Amazon.LakeFormation.Model;
using Amazon.Runtime;

namespace CloudOps.LakeFormation
{
    public class ListPermissionsOperation : Operation
    {
        public override string Name => "ListPermissions";

        public override string Description => "Returns a list of the principal permissions on the resource, filtered by the permissions of the caller. For example, if you are granted an ALTER permission, you are able to see only the principal permissions for ALTER. This operation returns only those permissions that have been explicitly granted. For information about permissions, see Security and Access Control to Metadata and Data.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "LakeFormation";

        public override string ServiceID => "LakeFormation";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLakeFormationConfig config = new AmazonLakeFormationConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLakeFormationClient client = new AmazonLakeFormationClient(creds, config);
            
            ListPermissionsResponse resp = new ListPermissionsResponse();
            do
            {
                try
                {
                    ListPermissionsRequest req = new ListPermissionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListPermissionsAsync(req);
                    
                    foreach (var obj in resp.PrincipalResourcePermissions)
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
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}