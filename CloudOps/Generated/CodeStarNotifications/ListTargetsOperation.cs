using Amazon;
using Amazon.CodeStarNotifications;
using Amazon.CodeStarNotifications.Model;
using Amazon.Runtime;

namespace CloudOps.CodeStarNotifications
{
    public class ListTargetsOperation : Operation
    {
        public override string Name => "ListTargets";

        public override string Description => "Returns a list of the notification rule targets for an AWS account.";
 
        public override string RequestURI => "/listTargets";

        public override string Method => "POST";

        public override string ServiceName => "CodeStarNotifications";

        public override string ServiceID => "codestar notifications";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeStarNotificationsConfig config = new AmazonCodeStarNotificationsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeStarNotificationsClient client = new AmazonCodeStarNotificationsClient(creds, config);
            
            ListTargetsResponse resp = new ListTargetsResponse();
            do
            {
                ListTargetsRequest req = new ListTargetsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListTargets(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Targets)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}