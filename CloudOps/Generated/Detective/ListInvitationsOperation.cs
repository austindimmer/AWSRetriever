using Amazon;
using Amazon.Detective;
using Amazon.Detective.Model;
using Amazon.Runtime;

namespace CloudOps.Detective
{
    public class ListInvitationsOperation : Operation
    {
        public override string Name => "ListInvitations";

        public override string Description => "Retrieves the list of open and accepted behavior graph invitations for the member account. This operation can only be called by a member account. Open invitations are invitations that the member account has not responded to. The results do not include behavior graphs for which the member account declined the invitation. The results also do not include behavior graphs that the member account resigned from or was removed from.";
 
        public override string RequestURI => "/invitations/list";

        public override string Method => "POST";

        public override string ServiceName => "Detective";

        public override string ServiceID => "Detective";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDetectiveConfig config = new AmazonDetectiveConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDetectiveClient client = new AmazonDetectiveClient(creds, config);
            
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