using Amazon;
using Amazon.SESV2;
using Amazon.SESV2.Model;
using Amazon.Runtime;

namespace CloudOps.SESV2
{
    public class ListSuppressedDestinationsOperation : Operation
    {
        public override string Name => "ListSuppressedDestinations";

        public override string Description => "Retrieves a list of email addresses that are on the suppression list for your account.";
 
        public override string RequestURI => "/v2/email/suppression/addresses";

        public override string Method => "GET";

        public override string ServiceName => "SESV2";

        public override string ServiceID => "SESv2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSESV2Config config = new AmazonSESV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSESV2Client client = new AmazonSESV2Client(creds, config);
            
            ListSuppressedDestinationsResponse resp = new ListSuppressedDestinationsResponse();
            do
            {
                ListSuppressedDestinationsRequest req = new ListSuppressedDestinationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = client.ListSuppressedDestinations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SuppressedDestinationSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}