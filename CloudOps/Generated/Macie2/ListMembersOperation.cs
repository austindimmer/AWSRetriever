using Amazon;
using Amazon.Macie2;
using Amazon.Macie2.Model;
using Amazon.Runtime;

namespace CloudOps.Macie2
{
    public class ListMembersOperation : Operation
    {
        public override string Name => "ListMembers";

        public override string Description => "Retrieves information about the accounts that are associated with an Amazon Macie administrator account.";
 
        public override string RequestURI => "/members";

        public override string Method => "GET";

        public override string ServiceName => "Macie2";

        public override string ServiceID => "Macie2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacie2Config config = new AmazonMacie2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacie2Client client = new AmazonMacie2Client(creds, config);
            
            ListMembersResponse resp = new ListMembersResponse();
            do
            {
                ListMembersRequest req = new ListMembersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListMembers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Members)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}