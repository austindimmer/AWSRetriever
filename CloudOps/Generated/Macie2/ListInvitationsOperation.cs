using Amazon;
using Amazon.Macie2;
using Amazon.Macie2.Model;
using Amazon.Runtime;

namespace CloudOps.Macie2
{
    public class ListInvitationsOperation : Operation
    {
        public override string Name => "ListInvitations";

        public override string Description => "Retrieves information about all the Amazon Macie membership invitations that were received by an account.";
 
        public override string RequestURI => "/invitations";

        public override string Method => "GET";

        public override string ServiceName => "Macie2";

        public override string ServiceID => "Macie2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacie2Config config = new AmazonMacie2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacie2Client client = new AmazonMacie2Client(creds, config);
            
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