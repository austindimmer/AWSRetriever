using Amazon;
using Amazon.GuardDuty;
using Amazon.GuardDuty.Model;
using Amazon.Runtime;

namespace CloudOps.GuardDuty
{
    public class ListInvitationsOperation : Operation
    {
        public override string Name => "ListInvitations";

        public override string Description => "Lists all GuardDuty membership invitations that were sent to the current AWS account.";
 
        public override string RequestURI => "/invitation";

        public override string Method => "GET";

        public override string ServiceName => "GuardDuty";

        public override string ServiceID => "GuardDuty";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGuardDutyConfig config = new AmazonGuardDutyConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGuardDutyClient client = new AmazonGuardDutyClient(creds, config);
            
            ListInvitationsResponse resp = new ListInvitationsResponse();
            do
            {
                try
                {
                    ListInvitationsRequest req = new ListInvitationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListInvitationsAsync(req);
                    
                    foreach (var obj in resp.Invitations)
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