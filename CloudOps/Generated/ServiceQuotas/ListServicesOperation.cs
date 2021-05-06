using Amazon;
using Amazon.ServiceQuotas;
using Amazon.ServiceQuotas.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceQuotas
{
    public class ListServicesOperation : Operation
    {
        public override string Name => "ListServices";

        public override string Description => "Lists the names and codes for the services integrated with Service Quotas.";
 
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
            
            ListServicesResponse resp = new ListServicesResponse();
            do
            {
                try
                {
                    ListServicesRequest req = new ListServicesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListServicesAsync(req);
                    
                    foreach (var obj in resp.Services)
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