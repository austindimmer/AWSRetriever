using Amazon;
using Amazon.Macie2;
using Amazon.Macie2.Model;
using Amazon.Runtime;

namespace CloudOps.Macie2
{
    public class ListOrganizationAdminAccountsOperation : Operation
    {
        public override string Name => "ListOrganizationAdminAccounts";

        public override string Description => "Retrieves information about the delegated Amazon Macie administrator account for an AWS organization.";
 
        public override string RequestURI => "/admin";

        public override string Method => "GET";

        public override string ServiceName => "Macie2";

        public override string ServiceID => "Macie2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacie2Config config = new AmazonMacie2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacie2Client client = new AmazonMacie2Client(creds, config);
            
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