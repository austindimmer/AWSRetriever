using Amazon;
using Amazon.MigrationHubConfig;
using Amazon.MigrationHubConfig.Model;
using Amazon.Runtime;

namespace CloudOps.MigrationHubConfig
{
    public class DescribeHomeRegionControlsOperation : Operation
    {
        public override string Name => "DescribeHomeRegionControls";

        public override string Description => "This API permits filtering on the ControlId and HomeRegion fields.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "MigrationHubConfig";

        public override string ServiceID => "MigrationHub Config";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMigrationHubConfigConfig config = new AmazonMigrationHubConfigConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMigrationHubConfigClient client = new AmazonMigrationHubConfigClient(creds, config);
            
            DescribeHomeRegionControlsResponse resp = new DescribeHomeRegionControlsResponse();
            do
            {
                DescribeHomeRegionControlsRequest req = new DescribeHomeRegionControlsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeHomeRegionControls(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.HomeRegionControls)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}