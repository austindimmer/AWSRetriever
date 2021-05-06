using Amazon;
using Amazon.ServiceCatalog;
using Amazon.ServiceCatalog.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceCatalog
{
    public class ListProvisioningArtifactsForServiceActionOperation : Operation
    {
        public override string Name => "ListProvisioningArtifactsForServiceAction";

        public override string Description => "Lists all provisioning artifacts (also known as versions) for the specified self-service action.";
 
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
            
            ListProvisioningArtifactsForServiceActionResponse resp = new ListProvisioningArtifactsForServiceActionResponse();
            do
            {
                try
                {
                    ListProvisioningArtifactsForServiceActionRequest req = new ListProvisioningArtifactsForServiceActionRequest
                    {
                        PageToken = resp.NextPageToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListProvisioningArtifactsForServiceActionAsync(req);
                    
                    foreach (var obj in resp.ProvisioningArtifactViews)
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