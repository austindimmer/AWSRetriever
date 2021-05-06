using Amazon;
using Amazon.DocDB;
using Amazon.DocDB.Model;
using Amazon.Runtime;

namespace CloudOps.DocDB
{
    public class DescribeEventSubscriptionsOperation : Operation
    {
        public override string Name => "DescribeEventSubscriptions";

        public override string Description => "Lists all the subscription descriptions for a customer account. The description for a subscription includes SubscriptionName, SNSTopicARN, CustomerID, SourceType, SourceID, CreationTime, and Status. If you specify a SubscriptionName, lists the description for that subscription.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DocDB";

        public override string ServiceID => "DocDB";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDocDBConfig config = new AmazonDocDBConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDocDBClient client = new AmazonDocDBClient(creds, config);
            
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