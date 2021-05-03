using Amazon;
using Amazon.Chime;
using Amazon.Chime.Model;
using Amazon.Runtime;

namespace CloudOps.Chime
{
    public class ListPhoneNumberOrdersOperation : Operation
    {
        public override string Name => "ListPhoneNumberOrders";

        public override string Description => "Lists the phone number orders for the administrator&#39;s Amazon Chime account.";
 
        public override string RequestURI => "/phone-number-orders";

        public override string Method => "GET";

        public override string ServiceName => "Chime";

        public override string ServiceID => "Chime";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonChimeConfig config = new AmazonChimeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonChimeClient client = new AmazonChimeClient(creds, config);
            
            ListPhoneNumberOrdersResponse resp = new ListPhoneNumberOrdersResponse();
            do
            {
                ListPhoneNumberOrdersRequest req = new ListPhoneNumberOrdersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListPhoneNumberOrders(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PhoneNumberOrders)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}