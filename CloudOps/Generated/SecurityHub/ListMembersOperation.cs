using Amazon;
using Amazon.SecurityHub;
using Amazon.SecurityHub.Model;
using Amazon.Runtime;

namespace CloudOps.SecurityHub
{
    public class ListMembersOperation : Operation
    {
        public override string Name => "ListMembers";

        public override string Description => "Lists details about all member accounts for the current Security Hub administrator account. The results include both member accounts that belong to an organization and member accounts that were invited manually.";
 
        public override string RequestURI => "/members";

        public override string Method => "GET";

        public override string ServiceName => "SecurityHub";

        public override string ServiceID => "SecurityHub";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSecurityHubConfig config = new AmazonSecurityHubConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSecurityHubClient client = new AmazonSecurityHubClient(creds, config);
            
            ListMembersResponse resp = new ListMembersResponse();
            do
            {
                try
                {
                    ListMembersRequest req = new ListMembersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListMembersAsync(req);
                    
                    foreach (var obj in resp.Members)
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