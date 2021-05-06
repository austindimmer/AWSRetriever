using Amazon;
using Amazon.Macie;
using Amazon.Macie.Model;
using Amazon.Runtime;

namespace CloudOps.Macie
{
    public class ListOrganizationAdminAccountsOperation : Operation
    {
        public override string Name => "ListOrganizationAdminAccounts";

        public override string Description => "Retrieves information about the delegated Amazon Macie administrator account for an AWS organization.";
 
        public override string RequestURI => "/admin";

        public override string Method => "GET";

        public override string ServiceName => "Macie";

        public override string ServiceID => "Macie";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacieConfig config = new AmazonMacieConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacieClient client = new AmazonMacieClient(creds, config);
            
            ListOrganizationAdminAccountsResponse resp = new ListOrganizationAdminAccountsResponse();
            do
            {
                ListOrganizationAdminAccountsRequest req = new ListOrganizationAdminAccountsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListOrganizationAdminAccounts(req);
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