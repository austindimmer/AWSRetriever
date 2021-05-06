using Amazon;
using Amazon.DataPipeline;
using Amazon.DataPipeline.Model;
using Amazon.Runtime;

namespace CloudOps.DataPipeline
{
    public class DescribeObjectsOperation : Operation
    {
        public override string Name => "DescribeObjects";

        public override string Description => "Gets the object definitions for a set of objects associated with the pipeline. Object definitions are composed of a set of fields that define the properties of the object.";
 
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
            
            DescribeObjectsResponse resp = new DescribeObjectsResponse();
            do
            {
                DescribeObjectsRequest req = new DescribeObjectsRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = await client.DescribeObjectsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PipelineObjects)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}