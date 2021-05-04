using Amazon;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using Amazon.Runtime;

namespace CloudOps.IdentityManagement
{
    public class ListSAMLProvidersOperation : Operation
    {
        public override string Name => "ListSAMLProviders";

        public override string Description => "Lists the SAML provider resource objects defined in IAM in the account. IAM resource-listing operations return a subset of the available attributes for the resource. For example, this operation does not return tags, even though they are an attribute of the returned object. To view all of the information for a SAML provider, see GetSAMLProvider.   This operation requires Signature Version 4. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "IdentityManagement";

        public override string ServiceID => "IAM";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIdentityManagementServiceConfig config = new AmazonIdentityManagementServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIdentityManagementServiceClient client = new AmazonIdentityManagementServiceClient(creds, config);
            
            ListSAMLProvidersResponse resp = new ListSAMLProvidersResponse();
            ListSAMLProvidersRequest req = new ListSAMLProvidersRequest
            {                    
                                    
            };
            resp = client.ListSAMLProviders(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.SAMLProviderList)
            {
                AddObject(obj);
            }
            
        }
    }
}