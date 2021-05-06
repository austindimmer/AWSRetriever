using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeExportTasksOperation : Operation
    {
        public override string Name => "DescribeExportTasks";

        public override string Description => "Describes the specified export instance tasks or all of your export instance tasks.";
 
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
            
            DescribeExportTasksResponse resp = new DescribeExportTasksResponse();
            DescribeExportTasksRequest req = new DescribeExportTasksRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribeExportTasksAsync(req);
                
                foreach (var obj in resp.ExportTasks)
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
    }
}