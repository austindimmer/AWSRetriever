using Amazon;
using Amazon.MediaConvert;
using Amazon.MediaConvert.Model;
using Amazon.Runtime;

namespace CloudOps.MediaConvert
{
    public class ListQueuesOperation : Operation
    {
        public override string Name => "ListQueues";

        public override string Description => "Retrieve a JSON array of up to twenty of your queues. This will return the queues themselves, not just a list of them. To retrieve the next twenty queues, use the nextToken string returned with the array.";
 
        public override string RequestURI => "/2017-08-29/queues";

        public override string Method => "GET";

        public override string ServiceName => "MediaConvert";

        public override string ServiceID => "MediaConvert";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaConvertConfig config = new AmazonMediaConvertConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaConvertClient client = new AmazonMediaConvertClient(creds, config);
            
            ListQueuesResponse resp = new ListQueuesResponse();
            do
            {
                ListQueuesRequest req = new ListQueuesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListQueuesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Queues)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}