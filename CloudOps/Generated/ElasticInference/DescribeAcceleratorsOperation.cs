using Amazon;
using Amazon.ElasticInference;
using Amazon.ElasticInference.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticInference
{
    public class DescribeAcceleratorsOperation : Operation
    {
        public override string Name => "DescribeAccelerators";

        public override string Description => " Describes information over a provided set of accelerators belonging to an account. ";
 
        public override string RequestURI => "/describe-accelerators";

        public override string Method => "POST";

        public override string ServiceName => "ElasticInference";

        public override string ServiceID => "Elastic Inference";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticInferenceConfig config = new AmazonElasticInferenceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticInferenceClient client = new AmazonElasticInferenceClient(creds, config);
            
            DescribeAcceleratorsResponse resp = new DescribeAcceleratorsResponse();
            do
            {
                DescribeAcceleratorsRequest req = new DescribeAcceleratorsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeAccelerators(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AcceleratorSet)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}