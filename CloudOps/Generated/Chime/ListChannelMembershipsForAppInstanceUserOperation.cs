using Amazon;
using Amazon.Chime;
using Amazon.Chime.Model;
using Amazon.Runtime;

namespace CloudOps.Chime
{
    public class ListChannelMembershipsForAppInstanceUserOperation : Operation
    {
        public override string Name => "ListChannelMembershipsForAppInstanceUser";

        public override string Description => " Lists all channels that a particular AppInstanceUser is a part of. Only an AppInstanceAdmin can call the API with a user ARN that is not their own.   The x-amz-chime-bearer request header is mandatory. Use the AppInstanceUserArn of the user that makes the API call as the value in the header. ";
 
        public override string RequestURI => "/channels?scope=app-instance-user-memberships";

        public override string Method => "GET";

        public override string ServiceName => "Chime";

        public override string ServiceID => "Chime";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonChimeConfig config = new AmazonChimeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonChimeClient client = new AmazonChimeClient(creds, config);
            
            ListChannelMembershipsForAppInstanceUserResponse resp = new ListChannelMembershipsForAppInstanceUserResponse();
            do
            {
                ListChannelMembershipsForAppInstanceUserRequest req = new ListChannelMembershipsForAppInstanceUserRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListChannelMembershipsForAppInstanceUser(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ChannelMemberships)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}