using Amazon;
using Amazon.ElasticMapReduce;
using Amazon.ElasticMapReduce.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticMapReduce
{
    public class ListStudiosOperation : Operation
    {
        public override string Name => "ListStudios";

        public override string Description => "Returns a list of all Amazon ElasticMapReduce Studios associated with the AWS account. The list includes details such as ID, Studio Access URL, and creation time for each Studio.";
 
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
            
            ListStudiosResponse resp = new ListStudiosResponse();
            do
            {
                ListStudiosRequest req = new ListStudiosRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = await client.ListStudiosAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Studios)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}