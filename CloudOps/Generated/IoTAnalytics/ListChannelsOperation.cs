using Amazon;
using Amazon.IoTAnalytics;
using Amazon.IoTAnalytics.Model;
using Amazon.Runtime;

namespace CloudOps.IoTAnalytics
{
    public class ListChannelsOperation : Operation
    {
        public override string Name => "ListChannels";

        public override string Description => "Retrieves a list of channels.";
 
        public override string RequestURI => "/channels";

        public override string Method => "GET";

        public override string ServiceName => "IoTAnalytics";

        public override string ServiceID => "IoTAnalytics";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTAnalyticsConfig config = new AmazonIoTAnalyticsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTAnalyticsClient client = new AmazonIoTAnalyticsClient(creds, config);
            
            ListChannelsResponse resp = new ListChannelsResponse();
            do
            {
                ListChannelsRequest req = new ListChannelsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListChannelsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ChannelSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}