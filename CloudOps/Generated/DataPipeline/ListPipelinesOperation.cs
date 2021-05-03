using Amazon;
using Amazon.DataPipeline;
using Amazon.DataPipeline.Model;
using Amazon.Runtime;

namespace CloudOps.DataPipeline
{
    public class ListPipelinesOperation : Operation
    {
        public override string Name => "ListPipelines";

        public override string Description => "Lists the pipeline identifiers for all active pipelines that you have permission to access.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DataPipeline";

        public override string ServiceID => "Data Pipeline";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDataPipelineConfig config = new AmazonDataPipelineConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDataPipelineClient client = new AmazonDataPipelineClient(creds, config);
            
            ListPipelinesResponse resp = new ListPipelinesResponse();
            do
            {
                ListPipelinesRequest req = new ListPipelinesRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = client.ListPipelines(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PipelineIdList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}