using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListStreamsOperation : Operation
    {
        public override string Name => "ListStreams";

        public override string Description => "Lists all of the streams in your AWS account.";
 
        public override string RequestURI => "/streams";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
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
                
                foreach (var obj in resp.Streams)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}