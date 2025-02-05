using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleSystemsManagement
{
    public class DescribeInventoryDeletionsOperation : Operation
    {
        public override string Name => "DescribeInventoryDeletions";

        public override string Description => "Describes a specific delete inventory operation.";
 
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
            
            DescribeInventoryDeletionsResponse resp = new DescribeInventoryDeletionsResponse();
            do
            {
                DescribeInventoryDeletionsRequest req = new DescribeInventoryDeletionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeInventoryDeletions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.InventoryDeletions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}