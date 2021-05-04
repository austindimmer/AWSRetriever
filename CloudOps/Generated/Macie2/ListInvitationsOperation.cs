using Amazon;
using Amazon.Macie;
using Amazon.Macie.Model;
using Amazon.Runtime;

namespace CloudOps.Macie
{
    public class ListInvitationsOperation : Operation
    {
        public override string Name => "ListInvitations";

        public override string Description => "Retrieves information about all the Amazon Macie membership invitations that were received by an account.";
 
        public override string RequestURI => "/invitations";

        public override string Method => "GET";

        public override string ServiceName => "Macie";

        public override string ServiceID => "Macie";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacieConfig config = new AmazonMacieConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacieClient client = new AmazonMacieClient(creds, config);
            
            ListInvitationsResponse resp = new ListInvitationsResponse();
            do
            {
                ListInvitationsRequest req = new ListInvitationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListInvitations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Invitations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}