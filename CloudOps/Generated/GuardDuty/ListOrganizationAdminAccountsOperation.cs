using Amazon;
using Amazon.GuardDuty;
using Amazon.GuardDuty.Model;
using Amazon.Runtime;

namespace CloudOps.GuardDuty
{
    public class ListOrganizationAdminAccountsOperation : Operation
    {
        public override string Name => "ListOrganizationAdminAccounts";

        public override string Description => "Lists the accounts configured as GuardDuty delegated administrators.";
 
        public override string RequestURI => "/admin";

        public override string Method => "GET";

        public override string ServiceName => "GuardDuty";

        public override string ServiceID => "GuardDuty";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGuardDutyConfig config = new AmazonGuardDutyConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGuardDutyClient client = new AmazonGuardDutyClient(creds, config);
            
            ListOrganizationAdminAccountsResponse resp = new ListOrganizationAdminAccountsResponse();
            do
            {
                try
                {
                    ListOrganizationAdminAccountsRequest req = new ListOrganizationAdminAccountsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListOrganizationAdminAccountsAsync(req);
                    
                    foreach (var obj in resp.AdminAccounts)
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