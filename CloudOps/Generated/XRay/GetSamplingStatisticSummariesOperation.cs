using Amazon;
using Amazon.XRay;
using Amazon.XRay.Model;
using Amazon.Runtime;

namespace CloudOps.XRay
{
    public class GetSamplingStatisticSummariesOperation : Operation
    {
        public override string Name => "GetSamplingStatisticSummaries";

        public override string Description => "Retrieves information about recent sampling results for all sampling rules.";
 
        public override string RequestURI => "/SamplingStatisticSummaries";

        public override string Method => "POST";

        public override string ServiceName => "XRay";

        public override string ServiceID => "XRay";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonXRayConfig config = new AmazonXRayConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonXRayClient client = new AmazonXRayClient(creds, config);
            
            GetSamplingStatisticSummariesResponse resp = new GetSamplingStatisticSummariesResponse();
            do
            {
                GetSamplingStatisticSummariesRequest req = new GetSamplingStatisticSummariesRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = client.GetSamplingStatisticSummaries(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SamplingStatisticSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}