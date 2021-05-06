using Amazon;
using Amazon.AuditManager;
using Amazon.AuditManager.Model;
using Amazon.Runtime;

namespace CloudOps.AuditManager
{
    public class ListNotificationsOperation : Operation
    {
        public override string Name => "ListNotifications";

        public override string Description => " Returns a list of all AWS Audit Manager notifications. ";
 
        public override string RequestURI => "/notifications";

        public override string Method => "GET";

        public override string ServiceName => "AuditManager";

        public override string ServiceID => "AuditManager";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAuditManagerConfig config = new AmazonAuditManagerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAuditManagerClient client = new AmazonAuditManagerClient(creds, config);
            
            ListNotificationsResponse resp = new ListNotificationsResponse();
            do
            {
                ListNotificationsRequest req = new ListNotificationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListNotificationsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Notifications)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}