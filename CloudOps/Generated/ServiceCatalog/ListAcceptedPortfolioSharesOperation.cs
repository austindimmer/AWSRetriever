using Amazon;
using Amazon.ServiceCatalog;
using Amazon.ServiceCatalog.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceCatalog
{
    public class ListAcceptedPortfolioSharesOperation : Operation
    {
        public override string Name => "ListAcceptedPortfolioShares";

        public override string Description => "Lists all portfolios for which sharing was accepted by this account.";
 
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
            
            ListAcceptedPortfolioSharesResponse resp = new ListAcceptedPortfolioSharesResponse();
            do
            {
                ListAcceptedPortfolioSharesRequest req = new ListAcceptedPortfolioSharesRequest
                {
                    PageToken = resp.NextPageToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = await client.ListAcceptedPortfolioSharesAsync(req);
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