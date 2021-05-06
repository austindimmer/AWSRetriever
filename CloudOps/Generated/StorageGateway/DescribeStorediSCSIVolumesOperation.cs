using Amazon;
using Amazon.StorageGateway;
using Amazon.StorageGateway.Model;
using Amazon.Runtime;

namespace CloudOps.StorageGateway
{
    public class DescribeStorediSCSIVolumesOperation : Operation
    {
        public override string Name => "DescribeStorediSCSIVolumes";

        public override string Description => "Returns the description of the gateway volumes specified in the request. The list of gateway volumes in the request must be from one gateway. In the response, AWS Storage Gateway returns volume information sorted by volume ARNs. This operation is only supported in stored volume gateway type.";
 
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
            
            DescribeStorediSCSIVolumesResponse resp = new DescribeStorediSCSIVolumesResponse();
            DescribeStorediSCSIVolumesRequest req = new DescribeStorediSCSIVolumesRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribeStorediSCSIVolumesAsync(req);
                
                foreach (var obj in resp.StorediSCSIVolumes)
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
    }
}