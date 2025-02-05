using Amazon;
using Amazon.ApplicationDiscoveryService;
using Amazon.ApplicationDiscoveryService.Model;
using Amazon.Runtime;

namespace CloudOps.ApplicationDiscoveryService
{
    public class DescribeImportTasksOperation : Operation
    {
        public override string Name => "DescribeImportTasks";

        public override string Description => "Returns an array of import tasks for your account, including status information, times, IDs, the Amazon S3 Object URL for the import file, and more.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ApplicationDiscoveryService";

        public override string ServiceID => "Application Discovery Service";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonApplicationDiscoveryServiceConfig config = new AmazonApplicationDiscoveryServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonApplicationDiscoveryServiceClient client = new AmazonApplicationDiscoveryServiceClient(creds, config);
            
            DescribeImportTasksResponse resp = new DescribeImportTasksResponse();
            do
            {
                DescribeImportTasksRequest req = new DescribeImportTasksRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeImportTasks(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Tasks)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}