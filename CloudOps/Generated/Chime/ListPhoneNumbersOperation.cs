using Amazon;
using Amazon.Chime;
using Amazon.Chime.Model;
using Amazon.Runtime;

namespace CloudOps.Chime
{
    public class ListPhoneNumbersOperation : Operation
    {
        public override string Name => "ListPhoneNumbers";

        public override string Description => "Lists the phone numbers for the specified Amazon Chime account, Amazon Chime user, Amazon Chime Voice Connector, or Amazon Chime Voice Connector group.";
 
        public override string RequestURI => "/phone-numbers";

        public override string Method => "GET";

        public override string ServiceName => "Chime";

        public override string ServiceID => "Chime";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonChimeConfig config = new AmazonChimeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonChimeClient client = new AmazonChimeClient(creds, config);
            
            ListPhoneNumbersResponse resp = new ListPhoneNumbersResponse();
            do
            {
                try
                {
                    ListPhoneNumbersRequest req = new ListPhoneNumbersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListPhoneNumbersAsync(req);
                    
                    foreach (var obj in resp.PhoneNumbers)
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