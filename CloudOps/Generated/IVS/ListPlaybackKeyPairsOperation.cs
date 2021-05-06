using Amazon;
using Amazon.IVS;
using Amazon.IVS.Model;
using Amazon.Runtime;

namespace CloudOps.IVS
{
    public class ListPlaybackKeyPairsOperation : Operation
    {
        public override string Name => "ListPlaybackKeyPairs";

        public override string Description => "Gets summary information about playback key pairs. For more information, see Setting Up Private Channels in the Amazon IVS User Guide.";
 
        public override string RequestURI => "/ListPlaybackKeyPairs";

        public override string Method => "POST";

        public override string ServiceName => "IVS";

        public override string ServiceID => "ivs";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIVSConfig config = new AmazonIVSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIVSClient client = new AmazonIVSClient(creds, config);
            
            ListPlaybackKeyPairsResponse resp = new ListPlaybackKeyPairsResponse();
            do
            {
                try
                {
                    ListPlaybackKeyPairsRequest req = new ListPlaybackKeyPairsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListPlaybackKeyPairsAsync(req);
                    
                    foreach (var obj in resp.KeyPairs)
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