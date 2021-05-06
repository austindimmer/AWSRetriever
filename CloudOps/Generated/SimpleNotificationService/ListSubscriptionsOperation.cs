using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleNotificationService
{
    public class ListSubscriptionsOperation : Operation
    {
        public override string Name => "ListSubscriptions";

        public override string Description => "Returns a list of the requester&#39;s subscriptions. Each call returns a limited list of subscriptions, up to 100. If there are more subscriptions, a NextToken is also returned. Use the NextToken parameter in a new ListSubscriptions call to get further results. This action is throttled at 30 transactions per second (TPS).";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SimpleNotificationService";

        public override string ServiceID => "SNS";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleNotificationServiceConfig config = new AmazonSimpleNotificationServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleNotificationServiceClient client = new AmazonSimpleNotificationServiceClient(creds, config);
            
            ListSubscriptionsResponse resp = new ListSubscriptionsResponse();
            do
            {
                try
                {
                    ListSubscriptionsRequest req = new ListSubscriptionsRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListSubscriptionsAsync(req);
                    
                    foreach (var obj in resp.Subscriptions)
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