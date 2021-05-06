using Amazon;
using Amazon.Chime;
using Amazon.Chime.Model;
using Amazon.Runtime;

namespace CloudOps.Chime
{
    public class ListVoiceConnectorGroupsOperation : Operation
    {
        public override string Name => "ListVoiceConnectorGroups";

        public override string Description => "Lists the Amazon Chime Voice Connector groups for the administrator&#39;s AWS account.";
 
        public override string RequestURI => "/voice-connector-groups";

        public override string Method => "GET";

        public override string ServiceName => "Chime";

        public override string ServiceID => "Chime";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonChimeConfig config = new AmazonChimeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonChimeClient client = new AmazonChimeClient(creds, config);
            
            ListVoiceConnectorGroupsResponse resp = new ListVoiceConnectorGroupsResponse();
            do
            {
                try
                {
                    ListVoiceConnectorGroupsRequest req = new ListVoiceConnectorGroupsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListVoiceConnectorGroupsAsync(req);
                    
                    foreach (var obj in resp.VoiceConnectorGroups)
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