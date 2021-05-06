using Amazon;
using Amazon.MediaLive;
using Amazon.MediaLive.Model;
using Amazon.Runtime;

namespace CloudOps.MediaLive
{
    public class ListInputSecurityGroupsOperation : Operation
    {
        public override string Name => "ListInputSecurityGroups";

        public override string Description => "Produces a list of Input Security Groups for an account";
 
        public override string RequestURI => "/prod/inputSecurityGroups";

        public override string Method => "GET";

        public override string ServiceName => "MediaLive";

        public override string ServiceID => "MediaLive";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaLiveConfig config = new AmazonMediaLiveConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaLiveClient client = new AmazonMediaLiveClient(creds, config);
            
            ListInputSecurityGroupsResponse resp = new ListInputSecurityGroupsResponse();
            do
            {
                ListInputSecurityGroupsRequest req = new ListInputSecurityGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListInputSecurityGroupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.InputSecurityGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}