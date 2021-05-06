using Amazon;
using Amazon.Neptune;
using Amazon.Neptune.Model;
using Amazon.Runtime;

namespace CloudOps.Neptune
{
    public class DescribeEventSubscriptionsOperation : Operation
    {
        public override string Name => "DescribeEventSubscriptions";

        public override string Description => "Lists all the subscription descriptions for a customer account. The description for a subscription includes SubscriptionName, SNSTopicARN, CustomerID, SourceType, SourceID, CreationTime, and Status. If you specify a SubscriptionName, lists the description for that subscription.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Neptune";

        public override string ServiceID => "Neptune";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonNeptuneConfig config = new AmazonNeptuneConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonNeptuneClient client = new AmazonNeptuneClient(creds, config);
            
            DescribeEventSubscriptionsResponse resp = new DescribeEventSubscriptionsResponse();
            do
            {
                try
                {
                    DescribeEventSubscriptionsRequest req = new DescribeEventSubscriptionsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeEventSubscriptionsAsync(req);
                    
                    foreach (var obj in resp.EventSubscriptionsList)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}