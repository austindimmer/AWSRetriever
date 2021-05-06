using Amazon;
using Amazon.XRay;
using Amazon.XRay.Model;
using Amazon.Runtime;

namespace CloudOps.XRay
{
    public class GetSamplingRulesOperation : Operation
    {
        public override string Name => "GetSamplingRules";

        public override string Description => "Retrieves all sampling rules.";
 
        public override string RequestURI => "/GetSamplingRules";

        public override string Method => "POST";

        public override string ServiceName => "XRay";

        public override string ServiceID => "XRay";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonXRayConfig config = new AmazonXRayConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonXRayClient client = new AmazonXRayClient(creds, config);
            
            GetSamplingRulesResponse resp = new GetSamplingRulesResponse();
            do
            {
                GetSamplingRulesRequest req = new GetSamplingRulesRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = await client.GetSamplingRulesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SamplingRuleRecords)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}