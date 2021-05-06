using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListCACertificatesOperation : Operation
    {
        public override string Name => "ListCACertificates";

        public override string Description => "Lists the CA certificates registered for your AWS account. The results are paginated with a default page size of 25. You can use the returned marker to retrieve additional results.";
 
        public override string RequestURI => "/cacertificates";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListCACertificatesResponse resp = new ListCACertificatesResponse();
            do
            {
                try
                {
                    ListCACertificatesRequest req = new ListCACertificatesRequest
                    {
                        Marker = resp.NextMarker
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListCACertificatesAsync(req);
                    
                    foreach (var obj in resp.Certificates)
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