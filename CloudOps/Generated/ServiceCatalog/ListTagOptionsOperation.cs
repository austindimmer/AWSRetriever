using Amazon;
using Amazon.ServiceCatalog;
using Amazon.ServiceCatalog.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceCatalog
{
    public class ListTagOptionsOperation : Operation
    {
        public override string Name => "ListTagOptions";

        public override string Description => "Lists the specified TagOptions or all TagOptions.";
 
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
            
            ListTagOptionsResponse resp = new ListTagOptionsResponse();
            do
            {
                try
                {
                    ListTagOptionsRequest req = new ListTagOptionsRequest
                    {
                        PageToken = resp.PageToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListTagOptionsAsync(req);
                    
                    foreach (var obj in resp.TagOptionDetails)
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
            while (!string.IsNullOrEmpty(resp.PageToken));
        }
    }
}