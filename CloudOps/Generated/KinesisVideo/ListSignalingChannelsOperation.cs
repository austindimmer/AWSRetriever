using Amazon;
using Amazon.KinesisVideo;
using Amazon.KinesisVideo.Model;
using Amazon.Runtime;

namespace CloudOps.KinesisVideo
{
    public class ListSignalingChannelsOperation : Operation
    {
        public override string Name => "ListSignalingChannels";

        public override string Description => "Returns an array of ChannelInfo objects. Each object describes a signaling channel. To retrieve only those channels that satisfy a specific condition, you can specify a ChannelNameCondition.";
 
        public override string RequestURI => "/listSignalingChannels";

        public override string Method => "POST";

        public override string ServiceName => "KinesisVideo";

        public override string ServiceID => "Kinesis Video";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonKinesisVideoConfig config = new AmazonKinesisVideoConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonKinesisVideoClient client = new AmazonKinesisVideoClient(creds, config);
            
            ListSignalingChannelsResponse resp = new ListSignalingChannelsResponse();
            do
            {
                ListSignalingChannelsRequest req = new ListSignalingChannelsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListSignalingChannelsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ChannelInfoList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}