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

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonServiceQuotasConfig config = new AmazonServiceQuotasConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonServiceQuotasClient client = new AmazonServiceQuotasClient(creds, config);
            
            ListServiceQuotaIncreaseRequestsInTemplateResponse resp = new ListServiceQuotaIncreaseRequestsInTemplateResponse();
            do
            {
                try
                {
                    ListServiceQuotaIncreaseRequestsInTemplateRequest req = new ListServiceQuotaIncreaseRequestsInTemplateRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListServiceQuotaIncreaseRequestsInTemplateAsync(req);
                    
                    foreach (var obj in resp.ServiceQuotaIncreaseRequestInTemplateList)
                    {
                        AddObject(obj);
                    }
                    
                }
                catch (System.Exception)
                {
                    CheckError(resp.HttpStatusCode, "200");                
                    throw;
                }

            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}