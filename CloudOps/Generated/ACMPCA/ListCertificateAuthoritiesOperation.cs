using Amazon;
using Amazon.ACMPCA;
using Amazon.ACMPCA.Model;
using Amazon.Runtime;

namespace CloudOps.ACMPCA
{
    public class ListCertificateAuthoritiesOperation : Operation
    {
        public override string Name => "ListCertificateAuthorities";

        public override string Description => "Lists the private certificate authorities that you created by using the CreateCertificateAuthority action.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ACMPCA";

        public override string ServiceID => "ACM PCA";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonACMPCAConfig config = new AmazonACMPCAConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonACMPCAClient client = new AmazonACMPCAClient(creds, config);
            
            ListCertificateAuthoritiesResponse resp = new ListCertificateAuthoritiesResponse();
            do
            {
                ListCertificateAuthoritiesRequest req = new ListCertificateAuthoritiesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListCertificateAuthoritiesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CertificateAuthorities)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}