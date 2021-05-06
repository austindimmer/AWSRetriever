using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListOutgoingCertificatesOperation : Operation
    {
        public override string Name => "ListOutgoingCertificates";

        public override string Description => "Lists certificates that are being transferred but not yet accepted.";
 
        public override string RequestURI => "/certificates-out-going";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListOutgoingCertificatesResponse resp = new ListOutgoingCertificatesResponse();
            do
            {
                try
                {
                    ListOutgoingCertificatesRequest req = new ListOutgoingCertificatesRequest
                    {
                        Marker = resp.NextMarker
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListOutgoingCertificatesAsync(req);
                    
                    foreach (var obj in resp.OutgoingCertificates)
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
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}