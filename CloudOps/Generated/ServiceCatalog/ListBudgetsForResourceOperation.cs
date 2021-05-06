using Amazon;
using Amazon.ServiceCatalog;
using Amazon.ServiceCatalog.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceCatalog
{
    public class ListBudgetsForResourceOperation : Operation
    {
        public override string Name => "ListBudgetsForResource";

        public override string Description => "Lists all the budgets associated to the specified resource.";
 
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
            
            ListBudgetsForResourceResponse resp = new ListBudgetsForResourceResponse();
            do
            {
                try
                {
                    ListBudgetsForResourceRequest req = new ListBudgetsForResourceRequest
                    {
                        PageToken = resp.NextPageToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListBudgetsForResourceAsync(req);
                    
                    foreach (var obj in resp.Budgets)
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