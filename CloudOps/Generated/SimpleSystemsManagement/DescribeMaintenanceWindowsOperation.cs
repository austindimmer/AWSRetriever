using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleSystemsManagement
{
    public class DescribeMaintenanceWindowsOperation : Operation
    {
        public override string Name => "DescribeMaintenanceWindows";

        public override string Description => "Retrieves the maintenance windows in an AWS account.";
 
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
            
            DescribeMaintenanceWindowsResponse resp = new DescribeMaintenanceWindowsResponse();
            do
            {
                DescribeMaintenanceWindowsRequest req = new DescribeMaintenanceWindowsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeMaintenanceWindows(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.WindowIdentities)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}