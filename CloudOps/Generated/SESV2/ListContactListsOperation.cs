using Amazon;
using Amazon.SESV2;
using Amazon.SESV2.Model;
using Amazon.Runtime;

namespace CloudOps.SESV2
{
    public class ListContactListsOperation : Operation
    {
        public override string Name => "ListContactLists";

        public override string Description => "Lists all of the contact lists available.";
 
        public override string RequestURI => "/v2/email/contact-lists";

        public override string Method => "GET";

        public override string ServiceName => "SESV2";

        public override string ServiceID => "SESv2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSESV2Config config = new AmazonSESV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSESV2Client client = new AmazonSESV2Client(creds, config);
            
            ListContactListsResponse resp = new ListContactListsResponse();
            do
            {
                ListContactListsRequest req = new ListContactListsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = client.ListContactLists(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ContactLists)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}