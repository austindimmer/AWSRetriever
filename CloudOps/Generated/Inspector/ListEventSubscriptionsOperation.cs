using Amazon;
using Amazon.Inspector;
using Amazon.Inspector.Model;
using Amazon.Runtime;

namespace CloudOps.Inspector
{
    public class ListEventSubscriptionsOperation : Operation
    {
        public override string Name => "ListEventSubscriptions";

        public override string Description => "Lists all the event subscriptions for the assessment template that is specified by the ARN of the assessment template. For more information, see SubscribeToEvent and UnsubscribeFromEvent.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Inspector";

        public override string ServiceID => "Inspector";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonInspectorConfig config = new AmazonInspectorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonInspectorClient client = new AmazonInspectorClient(creds, config);
            
            ListEventSubscriptionsResponse resp = new ListEventSubscriptionsResponse();
            do
            {
                try
                {
                    ListEventSubscriptionsRequest req = new ListEventSubscriptionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListEventSubscriptionsAsync(req);
                    
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