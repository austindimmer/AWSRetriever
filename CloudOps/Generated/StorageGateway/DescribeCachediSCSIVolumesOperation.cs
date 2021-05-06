using Amazon;
using Amazon.StorageGateway;
using Amazon.StorageGateway.Model;
using Amazon.Runtime;

namespace CloudOps.StorageGateway
{
    public class DescribeCachediSCSIVolumesOperation : Operation
    {
        public override string Name => "DescribeCachediSCSIVolumes";

        public override string Description => "Returns a description of the gateway volumes specified in the request. This operation is only supported in the cached volume gateway types. The list of gateway volumes in the request must be from one gateway. In the response, AWS Storage Gateway returns volume information sorted by volume Amazon Resource Name (ARN).";
 
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
            
            DescribeCachediSCSIVolumesResponse resp = new DescribeCachediSCSIVolumesResponse();
            DescribeCachediSCSIVolumesRequest req = new DescribeCachediSCSIVolumesRequest
            {                    
                                    
            };
            resp = await client.DescribeCachediSCSIVolumesAsync(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.CachediSCSIVolumes)
            {
                AddObject(obj);
            }
            
        }
    }
}