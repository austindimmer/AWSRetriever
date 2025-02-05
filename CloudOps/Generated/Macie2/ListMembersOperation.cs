using Amazon;
using Amazon.Macie;
using Amazon.Macie.Model;
using Amazon.Runtime;

namespace CloudOps.Macie
{
    public class ListMembersOperation : Operation
    {
        public override string Name => "ListMembers";

        public override string Description => "Retrieves information about the accounts that are associated with an Amazon Macie administrator account.";
 
        public override string RequestURI => "/members";

        public override string Method => "GET";

        public override string ServiceName => "Macie";

        public override string ServiceID => "Macie";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacieConfig config = new AmazonMacieConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacieClient client = new AmazonMacieClient(creds, config);
            
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