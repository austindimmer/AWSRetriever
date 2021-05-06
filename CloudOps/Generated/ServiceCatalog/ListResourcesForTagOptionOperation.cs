using Amazon;
using Amazon.ServiceCatalog;
using Amazon.ServiceCatalog.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceCatalog
{
    public class ListResourcesForTagOptionOperation : Operation
    {
        public override string Name => "ListResourcesForTagOption";

        public override string Description => "Lists the resources associated with the specified TagOption.";
 
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
            
            ListResourcesForTagOptionResponse resp = new ListResourcesForTagOptionResponse();
            do
            {
                try
                {
                    ListResourcesForTagOptionRequest req = new ListResourcesForTagOptionRequest
                    {
                        PageToken = resp.PageToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListResourcesForTagOptionAsync(req);
                    
                    foreach (var obj in resp.ResourceDetails)
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