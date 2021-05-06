using Amazon;
using Amazon.MediaTailor;
using Amazon.MediaTailor.Model;
using Amazon.Runtime;

namespace CloudOps.MediaTailor
{
    public class ListChannelsOperation : Operation
    {
        public override string Name => "ListChannels";

        public override string Description => "Retrieves a list of channels that are associated with this account.";
 
        public override string RequestURI => "/channels";

        public override string Method => "GET";

        public override string ServiceName => "MediaTailor";

        public override string ServiceID => "MediaTailor";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaTailorConfig config = new AmazonMediaTailorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaTailorClient client = new AmazonMediaTailorClient(creds, config);
            
            ListChannelsResponse resp = new ListChannelsResponse();
            do
            {
                try
                {
                    ListChannelsRequest req = new ListChannelsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListChannelsAsync(req);
                    
                    foreach (var obj in resp.Items)
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