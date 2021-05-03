using Amazon;
using Amazon.ElasticMapReduce;
using Amazon.ElasticMapReduce.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticMapReduce
{
    public class ListInstancesOperation : Operation
    {
        public override string Name => "ListInstances";

        public override string Description => "Provides information for all active EC2 instances and EC2 instances terminated in the last 30 days, up to a maximum of 2,000. EC2 instances in any of the following states are considered active: AWAITING_FULFILLMENT, PROVISIONING, BOOTSTRAPPING, RUNNING.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ElasticMapReduce";

        public override string ServiceID => "ElasticMapReduce";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticMapReduceConfig config = new AmazonElasticMapReduceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticMapReduceClient client = new AmazonElasticMapReduceClient(creds, config);
            
            ListInstancesResponse resp = new ListInstancesResponse();
            do
            {
                ListInstancesRequest req = new ListInstancesRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = client.ListInstances(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Instances)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}