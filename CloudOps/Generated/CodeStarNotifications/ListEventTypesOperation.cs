using Amazon;
using Amazon.CodeStarNotifications;
using Amazon.CodeStarNotifications.Model;
using Amazon.Runtime;

namespace CloudOps.CodeStarNotifications
{
    public class ListEventTypesOperation : Operation
    {
        public override string Name => "ListEventTypes";

        public override string Description => "Returns information about the event types available for configuring notifications.";
 
        public override string RequestURI => "/listEventTypes";

        public override string Method => "POST";

        public override string ServiceName => "CodeStarNotifications";

        public override string ServiceID => "codestar notifications";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeStarNotificationsConfig config = new AmazonCodeStarNotificationsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeStarNotificationsClient client = new AmazonCodeStarNotificationsClient(creds, config);
            
            ListEventTypesResponse resp = new ListEventTypesResponse();
            do
            {
                try
                {
                    ListEventTypesRequest req = new ListEventTypesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListEventTypesAsync(req);
                    
                    foreach (var obj in resp.EventTypes)
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