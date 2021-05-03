using Amazon;
using Amazon.DataPipeline;
using Amazon.DataPipeline.Model;
using Amazon.Runtime;

namespace CloudOps.DataPipeline
{
    public class DescribePipelinesOperation : Operation
    {
        public override string Name => "DescribePipelines";

        public override string Description => "Retrieves metadata about one or more pipelines. The information retrieved includes the name of the pipeline, the pipeline identifier, its current state, and the user account that owns the pipeline. Using account credentials, you can retrieve metadata about pipelines that you or your IAM users have created. If you are using an IAM user account, you can retrieve metadata about only those pipelines for which you have read permissions. To retrieve the full pipeline definition instead of metadata about the pipeline, call GetPipelineDefinition.";
 
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
            
            DescribePipelinesResponse resp = new DescribePipelinesResponse();
            DescribePipelinesRequest req = new DescribePipelinesRequest
            {                    
                                    
            };
            resp = client.DescribePipelines(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.PipelineDescriptionList)
            {
                AddObject(obj);
            }
            
        }
    }
}