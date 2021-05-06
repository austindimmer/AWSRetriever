using Amazon;
using Amazon.StorageGateway;
using Amazon.StorageGateway.Model;
using Amazon.Runtime;

namespace CloudOps.StorageGateway
{
    public class DescribeTapeArchivesOperation : Operation
    {
        public override string Name => "DescribeTapeArchives";

        public override string Description => "Returns a description of specified virtual tapes in the virtual tape shelf (VTS). This operation is only supported in the tape gateway type. If a specific TapeARN is not specified, AWS Storage Gateway returns a description of all virtual tapes found in the VTS associated with your account.";
 
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
            
            DescribeTapeArchivesResponse resp = new DescribeTapeArchivesResponse();
            do
            {
                try
                {
                    DescribeTapeArchivesRequest req = new DescribeTapeArchivesRequest
                    {
                        Marker = resp.Marker
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.DescribeTapeArchivesAsync(req);
                    
                    foreach (var obj in resp.TapeArchives)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}