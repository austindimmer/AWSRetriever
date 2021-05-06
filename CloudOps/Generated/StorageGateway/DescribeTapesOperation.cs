using Amazon;
using Amazon.StorageGateway;
using Amazon.StorageGateway.Model;
using Amazon.Runtime;

namespace CloudOps.StorageGateway
{
    public class DescribeTapesOperation : Operation
    {
        public override string Name => "DescribeTapes";

        public override string Description => "Returns a description of the specified Amazon Resource Name (ARN) of virtual tapes. If a TapeARN is not specified, returns a description of all virtual tapes associated with the specified gateway. This operation is only supported in the tape gateway type.";
 
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
            
            DescribeTapesResponse resp = new DescribeTapesResponse();
            do
            {
                try
                {
                    DescribeTapesRequest req = new DescribeTapesRequest
                    {
                        Marker = resp.Marker
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.DescribeTapesAsync(req);
                    
                    foreach (var obj in resp.Tapes)
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