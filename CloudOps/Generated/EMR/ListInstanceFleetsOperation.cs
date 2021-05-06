using Amazon;
using Amazon.ElasticMapReduce;
using Amazon.ElasticMapReduce.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticMapReduce
{
    public class ListInstanceFleetsOperation : Operation
    {
        public override string Name => "ListInstanceFleets";

        public override string Description => "Lists all available details about the instance fleets in a cluster.  The instance fleet configuration is available only in Amazon ElasticMapReduce versions 4.8.0 and later, excluding 5.0.x versions. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ElasticMapReduce";

        public override string ServiceID => "ElasticMapReduce";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticMapReduceConfig config = new AmazonElasticMapReduceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);
            AmazonElasticMapReduceClient client = new AmazonElasticMapReduceClient(creds, config);
            
            ListInstanceFleetsResponse resp = new ListInstanceFleetsResponse();
            do
            {
                ListInstanceFleetsRequest req = new ListInstanceFleetsRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = await client.ListInstanceFleetsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.InstanceFleets)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}