using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListTopicRuleDestinationsOperation : Operation
    {
        public override string Name => "ListTopicRuleDestinations";

        public override string Description => "Lists all the topic rule destinations in your AWS account.";
 
        public override string RequestURI => "/destinations";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListTopicRuleDestinationsResponse resp = new ListTopicRuleDestinationsResponse();
            do
            {
                try
                {
                    ListTopicRuleDestinationsRequest req = new ListTopicRuleDestinationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListTopicRuleDestinationsAsync(req);
                    
                    foreach (var obj in resp.DestinationSummaries)
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