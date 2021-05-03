using Amazon;
using Amazon.Kafka;
using Amazon.Kafka.Model;
using Amazon.Runtime;

namespace CloudOps.Kafka
{
    public class ListClustersOperation : Operation
    {
        public override string Name => "ListClusters";

        public override string Description => "Returns a list of all the MSK clusters in the current Region.";
 
        public override string RequestURI => "/v1/clusters";

        public override string Method => "GET";

        public override string ServiceName => "Kafka";

        public override string ServiceID => "Kafka";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonKafkaConfig config = new AmazonKafkaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonKafkaClient client = new AmazonKafkaClient(creds, config);
            
            ListClustersResponse resp = new ListClustersResponse();
            do
            {
                ListClustersRequest req = new ListClustersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListClusters(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ClusterInfoList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}