using Amazon;
using Amazon.ElasticMapReduce;
using Amazon.ElasticMapReduce.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticMapReduce
{
    public class ListBootstrapActionsOperation : Operation
    {
        public override string Name => "ListBootstrapActions";

        public override string Description => "Provides information about the bootstrap actions associated with a cluster.";
 
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
            
            ListBootstrapActionsResponse resp = new ListBootstrapActionsResponse();
            do
            {
                ListBootstrapActionsRequest req = new ListBootstrapActionsRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = await client.ListBootstrapActionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.BootstrapActions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}