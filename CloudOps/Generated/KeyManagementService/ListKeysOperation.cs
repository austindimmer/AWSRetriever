using Amazon;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Amazon.Runtime;

namespace CloudOps.KeyManagementService
{
    public class ListKeysOperation : Operation
    {
        public override string Name => "ListKeys";

        public override string Description => "Gets a list of all customer master keys (CMKs) in the caller&#39;s AWS account and Region.  Cross-account use: No. You cannot perform this operation on a CMK in a different AWS account.  Required permissions: kms:ListKeys (IAM policy)  Related operations:     CreateKey     DescribeKey     ListAliases     ListResourceTags   ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "KeyManagementService";

        public override string ServiceID => "KMS";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonKeyManagementServiceConfig config = new AmazonKeyManagementServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonKeyManagementServiceClient client = new AmazonKeyManagementServiceClient(creds, config);
            
            ListKeysResponse resp = new ListKeysResponse();
            do
            {
                ListKeysRequest req = new ListKeysRequest
                {
                    Marker = resp.NextMarker
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.ListKeys(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Keys)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}