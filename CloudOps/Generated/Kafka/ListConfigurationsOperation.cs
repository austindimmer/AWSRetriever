using Amazon;
using Amazon.Kafka;
using Amazon.Kafka.Model;
using Amazon.Runtime;

namespace CloudOps.Kafka
{
    public class ListConfigurationsOperation : Operation
    {
        public override string Name => "ListConfigurations";

        public override string Description => "Returns a list of all the MSK configurations in this Region.";
 
        public override string RequestURI => "/v1/configurations";

        public override string Method => "GET";

        public override string ServiceName => "Kafka";

        public override string ServiceID => "Kafka";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonKafkaConfig config = new AmazonKafkaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonKafkaClient client = new AmazonKafkaClient(creds, config);
            
            ListConfigurationsResponse resp = new ListConfigurationsResponse();
            do
            {
                try
                {
                    ListConfigurationsRequest req = new ListConfigurationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListConfigurationsAsync(req);
                    
                    foreach (var obj in resp.Configurations)
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