using Amazon;
using Amazon.KinesisVideo;
using Amazon.KinesisVideo.Model;
using Amazon.Runtime;

namespace CloudOps.KinesisVideo
{
    public class ListStreamsOperation : Operation
    {
        public override string Name => "ListStreams";

        public override string Description => "Returns an array of StreamInfo objects. Each object describes a stream. To retrieve only streams that satisfy a specific condition, you can specify a StreamNameCondition. ";
 
        public override string RequestURI => "/listStreams";

        public override string Method => "POST";

        public override string ServiceName => "KinesisVideo";

        public override string ServiceID => "Kinesis Video";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonKinesisVideoConfig config = new AmazonKinesisVideoConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonKinesisVideoClient client = new AmazonKinesisVideoClient(creds, config);
            
            ListStreamsResponse resp = new ListStreamsResponse();
            do
            {
                ListStreamsRequest req = new ListStreamsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListStreams(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.StreamInfoList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}