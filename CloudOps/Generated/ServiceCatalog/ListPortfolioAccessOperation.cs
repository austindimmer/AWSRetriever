using Amazon;
using Amazon.ServiceCatalog;
using Amazon.ServiceCatalog.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceCatalog
{
    public class ListPortfolioAccessOperation : Operation
    {
        public override string Name => "ListPortfolioAccess";

        public override string Description => "Lists the account IDs that have access to the specified portfolio. A delegated admin can list the accounts that have access to the shared portfolio. Note that if a delegated admin is de-registered, they can no longer perform this operation.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ServiceCatalog";

        public override string ServiceID => "Service Catalog";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonServiceCatalogConfig config = new AmazonServiceCatalogConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonServiceCatalogClient client = new AmazonServiceCatalogClient(creds, config);
            
            ListPortfolioAccessResponse resp = new ListPortfolioAccessResponse();
            do
            {
                try
                {
                    ListPortfolioAccessRequest req = new ListPortfolioAccessRequest
                    {
                        PageToken = resp.NextPageToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListPortfolioAccessAsync(req);
                    
                    foreach (var obj in resp.AccountIds)
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
            while (!string.IsNullOrEmpty(resp.NextPageToken));
        }
    }
}