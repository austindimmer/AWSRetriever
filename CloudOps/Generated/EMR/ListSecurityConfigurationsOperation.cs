using Amazon;
using Amazon.ElasticMapReduce;
using Amazon.ElasticMapReduce.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticMapReduce
{
    public class ListSecurityConfigurationsOperation : Operation
    {
        public override string Name => "ListSecurityConfigurations";

        public override string Description => "Lists all the security configurations visible to this account, providing their creation dates and times, and their names. This call returns a maximum of 50 clusters per call, but returns a marker to track the paging of the cluster list across multiple ListSecurityConfigurations calls.";
 
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
            
            ListSecurityConfigurationsResponse resp = new ListSecurityConfigurationsResponse();
            do
            {
                ListSecurityConfigurationsRequest req = new ListSecurityConfigurationsRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = await client.ListSecurityConfigurationsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SecurityConfigurations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}