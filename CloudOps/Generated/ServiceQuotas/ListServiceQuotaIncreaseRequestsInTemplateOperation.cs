using Amazon;
using Amazon.ServiceQuotas;
using Amazon.ServiceQuotas.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceQuotas
{
    public class ListServiceQuotaIncreaseRequestsInTemplateOperation : Operation
    {
        public override string Name => "ListServiceQuotaIncreaseRequestsInTemplate";

        public override string Description => "Lists the quota increase requests in the specified quota request template.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ServiceQuotas";

        public override string ServiceID => "Service Quotas";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonServiceQuotasConfig config = new AmazonServiceQuotasConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonServiceQuotasClient client = new AmazonServiceQuotasClient(creds, config);
            
            ListServiceQuotaIncreaseRequestsInTemplateResponse resp = new ListServiceQuotaIncreaseRequestsInTemplateResponse();
            do
            {
                ListServiceQuotaIncreaseRequestsInTemplateRequest req = new ListServiceQuotaIncreaseRequestsInTemplateRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListServiceQuotaIncreaseRequestsInTemplate(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ServiceQuotaIncreaseRequestInTemplateList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}