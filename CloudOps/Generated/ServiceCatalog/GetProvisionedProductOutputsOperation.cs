using Amazon;
using Amazon.ServiceCatalog;
using Amazon.ServiceCatalog.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceCatalog
{
    public class GetProvisionedProductOutputsOperation : Operation
    {
        public override string Name => "GetProvisionedProductOutputs";

        public override string Description => "This API takes either a ProvisonedProductId or a ProvisionedProductName, along with a list of one or more output keys, and responds with the key/value pairs of those outputs.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ServiceCatalog";

        public override string ServiceID => "Service Catalog";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonServiceCatalogConfig config = new AmazonServiceCatalogConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonServiceCatalogClient client = new AmazonServiceCatalogClient(creds, config);
            
            GetProvisionedProductOutputsResponse resp = new GetProvisionedProductOutputsResponse();
            do
            {
                GetProvisionedProductOutputsRequest req = new GetProvisionedProductOutputsRequest
                {
                    PageToken = resp.NextPageToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = client.GetProvisionedProductOutputs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Outputs)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextPageToken));
        }
    }
}