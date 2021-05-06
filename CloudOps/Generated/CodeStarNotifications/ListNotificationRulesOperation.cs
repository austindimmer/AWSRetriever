using Amazon;
using Amazon.CodeStarNotifications;
using Amazon.CodeStarNotifications.Model;
using Amazon.Runtime;

namespace CloudOps.CodeStarNotifications
{
    public class ListNotificationRulesOperation : Operation
    {
        public override string Name => "ListNotificationRules";

        public override string Description => "Returns a list of the notification rules for an AWS account.";
 
        public override string RequestURI => "/listNotificationRules";

        public override string Method => "POST";

        public override string ServiceName => "CodeStarNotifications";

        public override string ServiceID => "codestar notifications";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeStarNotificationsConfig config = new AmazonCodeStarNotificationsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeStarNotificationsClient client = new AmazonCodeStarNotificationsClient(creds, config);
            
            ListNotificationRulesResponse resp = new ListNotificationRulesResponse();
            do
            {
                ListNotificationRulesRequest req = new ListNotificationRulesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListNotificationRulesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.NotificationRules)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}