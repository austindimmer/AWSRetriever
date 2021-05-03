using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeImportSnapshotTasksOperation : Operation
    {
        public override string Name => "DescribeImportSnapshotTasks";

        public override string Description => "Describes your import snapshot tasks.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            DescribeImportSnapshotTasksResponse resp = new DescribeImportSnapshotTasksResponse();
            do
            {
                DescribeImportSnapshotTasksRequest req = new DescribeImportSnapshotTasksRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeImportSnapshotTasks(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ImportSnapshotTasks)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}