using Amazon;
using Amazon.Chime;
using Amazon.Chime.Model;
using Amazon.Runtime;

namespace CloudOps.Chime
{
    public class ListVoiceConnectorsOperation : Operation
    {
        public override string Name => "ListVoiceConnectors";

        public override string Description => "Lists the Amazon Chime Voice Connectors for the administrator&#39;s AWS account.";
 
        public override string RequestURI => "/voice-connectors";

        public override string Method => "GET";

        public override string ServiceName => "Chime";

        public override string ServiceID => "Chime";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonChimeConfig config = new AmazonChimeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonChimeClient client = new AmazonChimeClient(creds, config);
            
            ListVoiceConnectorsResponse resp = new ListVoiceConnectorsResponse();
            do
            {
                try
                {
                    ListVoiceConnectorsRequest req = new ListVoiceConnectorsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListVoiceConnectorsAsync(req);
                    
                    foreach (var obj in resp.VoiceConnectors)
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