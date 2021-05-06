using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Amazon.Runtime;

namespace CloudOps.CloudWatchLogs
{
    public class DescribeLogGroupsOperation : Operation
    {
        public override string Name => "DescribeLogGroups";

        public override string Description => "Lists the specified log groups. You can list all your log groups or filter the results by prefix. The results are ASCII-sorted by log group name.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudWatchLogs";

        public override string ServiceID => "CloudWatch Logs";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudWatchLogsConfig config = new AmazonCloudWatchLogsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudWatchLogsClient client = new AmazonCloudWatchLogsClient(creds, config);
            
            DescribeLogGroupsResponse resp = new DescribeLogGroupsResponse();
            do
            {
                DescribeLogGroupsRequest req = new DescribeLogGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.DescribeLogGroupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.LogGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}