using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeExportImageTasksOperation : Operation
    {
        public override string Name => "DescribeExportImageTasks";

        public override string Description => "Describes the specified export image tasks or all of your export image tasks.";
 
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
            
            DescribeExportImageTasksResponse resp = new DescribeExportImageTasksResponse();
            do
            {
                DescribeExportImageTasksRequest req = new DescribeExportImageTasksRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeExportImageTasks(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ExportImageTasks)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}