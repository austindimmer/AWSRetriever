using Amazon;
using Amazon.Chime;
using Amazon.Chime.Model;
using Amazon.Runtime;

namespace CloudOps.Chime
{
    public class ListChannelsModeratedByAppInstanceUserOperation : Operation
    {
        public override string Name => "ListChannelsModeratedByAppInstanceUser";

        public override string Description => "A list of the channels moderated by an AppInstanceUser.  The x-amz-chime-bearer request header is mandatory. Use the AppInstanceUserArn of the user that makes the API call as the value in the header. ";
 
        public override string RequestURI => "/channels?scope=app-instance-user-moderated-channels";

        public override string Method => "GET";

        public override string ServiceName => "Chime";

        public override string ServiceID => "Chime";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonChimeConfig config = new AmazonChimeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonChimeClient client = new AmazonChimeClient(creds, config);
            
            ListChannelsModeratedByAppInstanceUserResponse resp = new ListChannelsModeratedByAppInstanceUserResponse();
            do
            {
                try
                {
                    ListChannelsModeratedByAppInstanceUserRequest req = new ListChannelsModeratedByAppInstanceUserRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListChannelsModeratedByAppInstanceUserAsync(req);
                    
                    foreach (var obj in resp.Channels)
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