using Amazon;
using Amazon.Runtime;

namespace CloudOps.ACM
{
    public class ListCertificatesOperation : Operation
    {
        public override string Name => "ListCertificates";

        public override string Description => "Retrieves a list of certificate ARNs and domain names. You can request that only certificates that match a specific status be listed. You can also filter by specific attributes of the certificate. Default filtering returns only RSA_2048 certificates. For more information, see Filters.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ACM";

        public override string ServiceID => "ACM";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonACMConfig config = new AmazonACMConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonACMClient client = new AmazonACMClient(creds, config);
            
            ListCertificatesResponse resp = new ListCertificatesResponse();
            do
            {
                ListCertificatesRequest req = new ListCertificatesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = client.ListCertificates(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CertificateSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}