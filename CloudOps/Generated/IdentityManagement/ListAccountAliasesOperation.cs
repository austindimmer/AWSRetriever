using Amazon;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using Amazon.Runtime;

namespace CloudOps.IdentityManagement
{
    public class ListAccountAliasesOperation : Operation
    {
        public override string Name => "ListAccountAliases";

        public override string Description => "Lists the account alias associated with the AWS account (Note: you can have only one). For information about using an AWS account alias, see Using an alias for your AWS account ID in the IAM User Guide.";
 
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
            
            ListAccountAliasesResponse resp = new ListAccountAliasesResponse();
            do
            {
                try
                {
                    ListAccountAliasesRequest req = new ListAccountAliasesRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxItems = maxItems
                                            
                    };

                    resp = await client.ListAccountAliasesAsync(req);
                    
                    foreach (var obj in resp.AccountAliases)
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