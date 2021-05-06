using Amazon;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using Amazon.Runtime;

namespace CloudOps.IdentityManagement
{
    public class ListSigningCertificatesOperation : Operation
    {
        public override string Name => "ListSigningCertificates";

        public override string Description => "Returns information about the signing certificates associated with the specified IAM user. If none exists, the operation returns an empty list. Although each user is limited to a small number of signing certificates, you can still paginate the results using the MaxItems and Marker parameters. If the UserName field is not specified, the user name is determined implicitly based on the AWS access key ID used to sign the request for this operation. This operation works for access keys under the AWS account. Consequently, you can use this operation to manage AWS account root user credentials even if the AWS account has no associated users.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "IdentityManagement";

        public override string ServiceID => "IAM";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIdentityManagementServiceConfig config = new AmazonIdentityManagementServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIdentityManagementServiceClient client = new AmazonIdentityManagementServiceClient(creds, config);
            
            ListSigningCertificatesResponse resp = new ListSigningCertificatesResponse();
            do
            {
                ListSigningCertificatesRequest req = new ListSigningCertificatesRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = await client.ListSigningCertificatesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Certificates)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}