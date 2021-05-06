using Amazon;
using Amazon.ServiceQuotas;
using Amazon.ServiceQuotas.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceQuotas
{
    public class ListRequestedServiceQuotaChangeHistoryOperation : Operation
    {
        public override string Name => "ListRequestedServiceQuotaChangeHistory";

        public override string Description => "Retrieves the quota increase requests for the specified service.";
 
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
            
            ListRequestedServiceQuotaChangeHistoryResponse resp = new ListRequestedServiceQuotaChangeHistoryResponse();
            do
            {
                ListRequestedServiceQuotaChangeHistoryRequest req = new ListRequestedServiceQuotaChangeHistoryRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListRequestedServiceQuotaChangeHistoryAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.RequestedQuotas)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}