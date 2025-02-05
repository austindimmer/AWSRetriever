using Amazon;
using Amazon.SecurityHub;
using Amazon.SecurityHub.Model;
using Amazon.Runtime;

namespace CloudOps.SecurityHub
{
    public class ListEnabledProductsForImportOperation : Operation
    {
        public override string Name => "ListEnabledProductsForImport";

        public override string Description => "Lists all findings-generating solutions (products) that you are subscribed to receive findings from in Security Hub.";
 
        public override string RequestURI => "/productSubscriptions";

        public override string Method => "GET";

        public override string ServiceName => "SecurityHub";

        public override string ServiceID => "SecurityHub";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSecurityHubConfig config = new AmazonSecurityHubConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSecurityHubClient client = new AmazonSecurityHubClient(creds, config);
            
            ListEnabledProductsForImportResponse resp = new ListEnabledProductsForImportResponse();
            do
            {
                ListEnabledProductsForImportRequest req = new ListEnabledProductsForImportRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListEnabledProductsForImport(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ProductSubscriptions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}