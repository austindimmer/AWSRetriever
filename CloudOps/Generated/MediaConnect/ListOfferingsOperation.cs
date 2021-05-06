using Amazon;
using Amazon.MediaConnect;
using Amazon.MediaConnect.Model;
using Amazon.Runtime;

namespace CloudOps.MediaConnect
{
    public class ListOfferingsOperation : Operation
    {
        public override string Name => "ListOfferings";

        public override string Description => "Displays a list of all offerings that are available to this account in the current AWS Region. If you have an active reservation (which means you&#39;ve purchased an offering that has already started and hasn&#39;t expired yet), your account isn&#39;t eligible for other offerings.";
 
        public override string RequestURI => "/v1/offerings";

        public override string Method => "GET";

        public override string ServiceName => "MediaConnect";

        public override string ServiceID => "MediaConnect";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaConnectConfig config = new AmazonMediaConnectConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaConnectClient client = new AmazonMediaConnectClient(creds, config);
            
            ListOfferingsResponse resp = new ListOfferingsResponse();
            do
            {
                try
                {
                    ListOfferingsRequest req = new ListOfferingsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListOfferingsAsync(req);
                    
                    foreach (var obj in resp.Offerings)
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