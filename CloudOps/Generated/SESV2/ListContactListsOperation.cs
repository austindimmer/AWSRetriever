using Amazon;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleEmailV2
{
    public class ListContactListsOperation : Operation
    {
        public override string Name => "ListContactLists";

        public override string Description => "Lists all of the contact lists available.";
 
        public override string RequestURI => "/v2/email/contact-lists";

        public override string Method => "GET";

        public override string ServiceName => "SimpleEmailV2";

        public override string ServiceID => "SimpleEmailV2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleEmailServiceV2Config config = new AmazonSimpleEmailServiceV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleEmailServiceV2Client client = new AmazonSimpleEmailServiceV2Client(creds, config);
            
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