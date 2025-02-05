using Amazon;
using Amazon.WellArchitected;
using Amazon.WellArchitected.Model;
using Amazon.Runtime;

namespace CloudOps.WellArchitected
{
    public class ListNotificationsOperation : Operation
    {
        public override string Name => "ListNotifications";

        public override string Description => "List lens notifications.";
 
        public override string RequestURI => "/notifications";

        public override string Method => "POST";

        public override string ServiceName => "WellArchitected";

        public override string ServiceID => "WellArchitected";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWellArchitectedConfig config = new AmazonWellArchitectedConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWellArchitectedClient client = new AmazonWellArchitectedClient(creds, config);
            
            ListNotificationsResponse resp = new ListNotificationsResponse();
            do
            {
                ListNotificationsRequest req = new ListNotificationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListNotifications(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.NotificationSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}