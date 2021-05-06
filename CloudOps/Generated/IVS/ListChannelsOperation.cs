using Amazon;
using Amazon.IVS;
using Amazon.IVS.Model;
using Amazon.Runtime;

namespace CloudOps.IVS
{
    public class ListChannelsOperation : Operation
    {
        public override string Name => "ListChannels";

        public override string Description => "Gets summary information about all channels in your account, in the AWS region where the API request is processed. This list can be filtered to match a specified name or recording-configuration ARN. Filters are mutually exclusive and cannot be used together. If you try to use both filters, you will get an error (409 ConflictException).";
 
        public override string RequestURI => "/ListChannels";

        public override string Method => "POST";

        public override string ServiceName => "IVS";

        public override string ServiceID => "ivs";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIVSConfig config = new AmazonIVSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIVSClient client = new AmazonIVSClient(creds, config);
            
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