using Amazon;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Amazon.Runtime;

namespace CloudOps.KeyManagementService
{
    public class ListAliasesOperation : Operation
    {
        public override string Name => "ListAliases";

        public override string Description => "Gets a list of aliases in the caller&#39;s AWS account and region. For more information about aliases, see CreateAlias. By default, the ListAliases operation returns all aliases in the account and region. To get only the aliases associated with a particular customer master key (CMK), use the KeyId parameter. The ListAliases response can include aliases that you created and associated with your customer managed CMKs, and aliases that AWS created and associated with AWS managed CMKs in your account. You can recognize AWS aliases because their names have the format aws/&amp;lt;service-name&amp;gt;, such as aws/dynamodb. The response might also include aliases that have no TargetKeyId field. These are predefined aliases that AWS has created but has not yet associated with a CMK. Aliases that AWS creates in your account, including predefined aliases, do not count against your AWS KMS aliases quota.  Cross-account use: No. ListAliases does not return aliases in other AWS accounts.  Required permissions: kms:ListAliases (IAM policy) For details, see Controlling access to aliases in the AWS Key Management Service Developer Guide.  Related operations:     CreateAlias     DeleteAlias     UpdateAlias   ";
 
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
            
            ListAliasesResponse resp = new ListAliasesResponse();
            do
            {
                ListAliasesRequest req = new ListAliasesRequest
                {
                    Marker = resp.NextMarker
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.ListAliases(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Aliases)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}