using Amazon;
using Amazon.DataPipeline;
using Amazon.DataPipeline.Model;
using Amazon.Runtime;

namespace CloudOps.DataPipeline
{
    public class QueryObjectsOperation : Operation
    {
        public override string Name => "QueryObjects";

        public override string Description => "Queries the specified pipeline for the names of objects that match the specified set of conditions.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DataPipeline";

        public override string ServiceID => "Data Pipeline";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDataPipelineConfig config = new AmazonDataPipelineConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDataPipelineClient client = new AmazonDataPipelineClient(creds, config);
            
            QueryObjectsResponse resp = new QueryObjectsResponse();
            do
            {
                QueryObjectsRequest req = new QueryObjectsRequest
                {
                    Marker = resp.Marker
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.QueryObjectsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Ids)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}