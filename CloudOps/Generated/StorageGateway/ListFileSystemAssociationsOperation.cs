using Amazon;
using Amazon.StorageGateway;
using Amazon.StorageGateway.Model;
using Amazon.Runtime;

namespace CloudOps.StorageGateway
{
    public class ListFileSystemAssociationsOperation : Operation
    {
        public override string Name => "ListFileSystemAssociations";

        public override string Description => "Gets a list of FileSystemAssociationSummary objects. Each object contains a summary of a file system association. This operation is only supported for Amazon FSx file gateways.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "StorageGateway";

        public override string ServiceID => "Storage Gateway";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonStorageGatewayConfig config = new AmazonStorageGatewayConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonStorageGatewayClient client = new AmazonStorageGatewayClient(creds, config);
            
            ListFileSystemAssociationsResponse resp = new ListFileSystemAssociationsResponse();
            do
            {
                ListFileSystemAssociationsRequest req = new ListFileSystemAssociationsRequest
                {
                    Marker = resp.NextMarker
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.ListFileSystemAssociationsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FileSystemAssociationSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}