using Amazon;
using Amazon.ElasticMapReduce;
using Amazon.ElasticMapReduce.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticMapReduce
{
    public class ListStudioSessionMappingsOperation : Operation
    {
        public override string Name => "ListStudioSessionMappings";

        public override string Description => "Returns a list of all user or group session mappings for the Amazon ElasticMapReduce Studio specified by StudioId.";
 
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
            
            ListStudioSessionMappingsResponse resp = new ListStudioSessionMappingsResponse();
            do
            {
                ListStudioSessionMappingsRequest req = new ListStudioSessionMappingsRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = await client.ListStudioSessionMappingsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SessionMappings)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}