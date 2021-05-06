using Amazon;
using Amazon.ServiceCatalog;
using Amazon.ServiceCatalog.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceCatalog
{
    public class ListPortfoliosForProductOperation : Operation
    {
        public override string Name => "ListPortfoliosForProduct";

        public override string Description => "Lists all portfolios that the specified product is associated with.";
 
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
            
            ListPortfoliosForProductResponse resp = new ListPortfoliosForProductResponse();
            do
            {
                ListPortfoliosForProductRequest req = new ListPortfoliosForProductRequest
                {
                    PageToken = resp.NextPageToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = await client.ListPortfoliosForProductAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PortfolioDetails)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextPageToken));
        }
    }
}