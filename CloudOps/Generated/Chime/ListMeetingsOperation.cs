using Amazon;
using Amazon.Chime;
using Amazon.Chime.Model;
using Amazon.Runtime;

namespace CloudOps.Chime
{
    public class ListMeetingsOperation : Operation
    {
        public override string Name => "ListMeetings";

        public override string Description => " Lists up to 100 active Amazon Chime SDK meetings. For more information about the Amazon Chime SDK, see Using the Amazon Chime SDK in the Amazon Chime Developer Guide . ";
 
        public override string RequestURI => "/meetings";

        public override string Method => "GET";

        public override string ServiceName => "Chime";

        public override string ServiceID => "Chime";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonChimeConfig config = new AmazonChimeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonChimeClient client = new AmazonChimeClient(creds, config);
            
            ListMeetingsResponse resp = new ListMeetingsResponse();
            do
            {
                try
                {
                    ListMeetingsRequest req = new ListMeetingsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListMeetingsAsync(req);
                    
                    foreach (var obj in resp.Meetings)
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