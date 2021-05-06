using Amazon;
using Amazon.MediaTailor;
using Amazon.MediaTailor.Model;
using Amazon.Runtime;

namespace CloudOps.MediaTailor
{
    public class ListPlaybackConfigurationsOperation : Operation
    {
        public override string Name => "ListPlaybackConfigurations";

        public override string Description => "Returns a list of the playback configurations defined in AWS Elemental MediaTailor. You can specify a maximum number of configurations to return at a time. The default maximum is 50. Results are returned in pagefuls. If MediaTailor has more configurations than the specified maximum, it provides parameters in the response that you can use to retrieve the next pageful.  ";
 
        public override string RequestURI => "/playbackConfigurations";

        public override string Method => "GET";

        public override string ServiceName => "MediaTailor";

        public override string ServiceID => "MediaTailor";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaTailorConfig config = new AmazonMediaTailorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaTailorClient client = new AmazonMediaTailorClient(creds, config);
            
            ListPlaybackConfigurationsResponse resp = new ListPlaybackConfigurationsResponse();
            do
            {
                try
                {
                    ListPlaybackConfigurationsRequest req = new ListPlaybackConfigurationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListPlaybackConfigurationsAsync(req);
                    
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