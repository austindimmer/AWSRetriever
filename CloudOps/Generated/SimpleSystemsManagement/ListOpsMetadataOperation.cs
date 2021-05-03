using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleSystemsManagement
{
    public class ListOpsMetadataOperation : Operation
    {
        public override string Name => "ListOpsMetadata";

        public override string Description => "Systems Manager calls this API action when displaying all Application Manager OpsMetadata objects or blobs.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SimpleSystemsManagement";

        public override string ServiceID => "SSM";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleSystemsManagementConfig config = new AmazonSimpleSystemsManagementConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleSystemsManagementClient client = new AmazonSimpleSystemsManagementClient(creds, config);
            
            ListOpsMetadataResponse resp = new ListOpsMetadataResponse();
            do
            {
                ListOpsMetadataRequest req = new ListOpsMetadataRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListOpsMetadata(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.OpsMetadataList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}