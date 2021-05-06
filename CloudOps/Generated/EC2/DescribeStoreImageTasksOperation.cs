using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeStoreImageTasksOperation : Operation
    {
        public override string Name => "DescribeStoreImageTasks";

        public override string Description => "Describes the progress of the AMI store tasks. You can describe the store tasks for specified AMIs. If you don&#39;t specify the AMIs, you get a paginated list of store tasks from the last 31 days. For each AMI task, the response indicates if the task is InProgress, Completed, or Failed. For tasks InProgress, the response shows the estimated progress as a percentage. Tasks are listed in reverse chronological order. Currently, only tasks from the past 31 days can be viewed. To use this API, you must have the required permissions. For more information, see Permissions for storing and restoring AMIs using S3 in the Amazon Elastic Compute Cloud User Guide. For more information, see Store and restore an AMI using S3 in the Amazon Elastic Compute Cloud User Guide.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            DescribeStoreImageTasksResponse resp = new DescribeStoreImageTasksResponse();
            do
            {
                try
                {
                    DescribeStoreImageTasksRequest req = new DescribeStoreImageTasksRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeStoreImageTasksAsync(req);
                    
                    foreach (var obj in resp.StoreImageTaskResults)
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