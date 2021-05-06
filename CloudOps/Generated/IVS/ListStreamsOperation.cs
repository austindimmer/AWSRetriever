using Amazon;
using Amazon.IVS;
using Amazon.IVS.Model;
using Amazon.Runtime;

namespace CloudOps.IVS
{
    public class ListStreamsOperation : Operation
    {
        public override string Name => "ListStreams";

        public override string Description => "Gets summary information about live streams in your account, in the AWS region where the API request is processed.";
 
        public override string RequestURI => "/ListStreams";

        public override string Method => "POST";

        public override string ServiceName => "IVS";

        public override string ServiceID => "ivs";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIVSConfig config = new AmazonIVSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIVSClient client = new AmazonIVSClient(creds, config);
            
            ListStreamsResponse resp = new ListStreamsResponse();
            do
            {
                try
                {
                    ListStreamsRequest req = new ListStreamsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListStreamsAsync(req);
                    
                    foreach (var obj in resp.Streams)
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