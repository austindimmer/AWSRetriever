using Amazon;
using Amazon.MediaPackage;
using Amazon.MediaPackage.Model;
using Amazon.Runtime;

namespace CloudOps.MediaPackage
{
    public class ListChannelsOperation : Operation
    {
        public override string Name => "ListChannels";

        public override string Description => "Returns a collection of Channels.";
 
        public override string RequestURI => "/channels";

        public override string Method => "GET";

        public override string ServiceName => "MediaPackage";

        public override string ServiceID => "MediaPackage";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaPackageConfig config = new AmazonMediaPackageConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaPackageClient client = new AmazonMediaPackageClient(creds, config);
            
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
                
                foreach (var obj in resp.Channels)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}