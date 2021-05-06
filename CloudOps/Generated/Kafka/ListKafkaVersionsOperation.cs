using Amazon;
using Amazon.Kafka;
using Amazon.Kafka.Model;
using Amazon.Runtime;

namespace CloudOps.Kafka
{
    public class ListKafkaVersionsOperation : Operation
    {
        public override string Name => "ListKafkaVersions";

        public override string Description => "Returns a list of Kafka versions.";
 
        public override string RequestURI => "/v1/kafka-versions";

        public override string Method => "GET";

        public override string ServiceName => "Kafka";

        public override string ServiceID => "Kafka";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonKafkaConfig config = new AmazonKafkaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonKafkaClient client = new AmazonKafkaClient(creds, config);
            
            ListKafkaVersionsResponse resp = new ListKafkaVersionsResponse();
            do
            {
                ListKafkaVersionsRequest req = new ListKafkaVersionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListKafkaVersionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.KafkaVersions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}