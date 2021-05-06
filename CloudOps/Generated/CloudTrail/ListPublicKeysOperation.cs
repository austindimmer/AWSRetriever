using Amazon;
using Amazon.CloudTrail;
using Amazon.CloudTrail.Model;
using Amazon.Runtime;

namespace CloudOps.CloudTrail
{
    public class ListPublicKeysOperation : Operation
    {
        public override string Name => "ListPublicKeys";

        public override string Description => "Returns all public keys whose private keys were used to sign the digest files within the specified time range. The public key is needed to validate digest files that were signed with its corresponding private key.  CloudTrail uses different private/public key pairs per region. Each digest file is signed with a private key unique to its region. Therefore, when you validate a digest file from a particular region, you must look in the same region for its corresponding public key. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudTrail";

        public override string ServiceID => "CloudTrail";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudTrailConfig config = new AmazonCloudTrailConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudTrailClient client = new AmazonCloudTrailClient(creds, config);
            
            ListPublicKeysResponse resp = new ListPublicKeysResponse();
            do
            {
                try
                {
                    ListPublicKeysRequest req = new ListPublicKeysRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListPublicKeysAsync(req);
                    
                    foreach (var obj in resp.PublicKeyList)
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
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}