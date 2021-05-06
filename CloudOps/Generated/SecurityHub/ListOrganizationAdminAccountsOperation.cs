using Amazon;
using Amazon.SecurityHub;
using Amazon.SecurityHub.Model;
using Amazon.Runtime;

namespace CloudOps.SecurityHub
{
    public class ListOrganizationAdminAccountsOperation : Operation
    {
        public override string Name => "ListOrganizationAdminAccounts";

        public override string Description => "Lists the Security Hub administrator accounts. Can only be called by the organization management account.";
 
        public override string RequestURI => "/organization/admin";

        public override string Method => "GET";

        public override string ServiceName => "SecurityHub";

        public override string ServiceID => "SecurityHub";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSecurityHubConfig config = new AmazonSecurityHubConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSecurityHubClient client = new AmazonSecurityHubClient(creds, config);
            
            ListOrganizationAdminAccountsResponse resp = new ListOrganizationAdminAccountsResponse();
            do
            {
                ListOrganizationAdminAccountsRequest req = new ListOrganizationAdminAccountsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListOrganizationAdminAccountsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AdminAccounts)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}