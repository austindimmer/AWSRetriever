using Amazon;
using Amazon.MQ;
using Amazon.MQ.Model;
using Amazon.Runtime;

namespace CloudOps.MQ
{
    public class ListBrokersOperation : Operation
    {
        public override string Name => "ListBrokers";

        public override string Description => "Returns a list of all brokers.";
 
        public override string RequestURI => "/v1/brokers";

        public override string Method => "GET";

        public override string ServiceName => "MQ";

        public override string ServiceID => "mq";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMQConfig config = new AmazonMQConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMQClient client = new AmazonMQClient(creds, config);
            
            ListBrokersResponse resp = new ListBrokersResponse();
            do
            {
                try
                {
                    ListBrokersRequest req = new ListBrokersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListBrokersAsync(req);
                    
                    foreach (var obj in resp.BrokerSummaries)
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