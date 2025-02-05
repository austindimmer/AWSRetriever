using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeReplaceRootVolumeTasksOperation : Operation
    {
        public override string Name => "DescribeReplaceRootVolumeTasks";

        public override string Description => "Describes a root volume replacement task. For more information, see Replace a root volume in the Amazon Elastic Compute Cloud User Guide.";
 
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
            
            DescribeReplaceRootVolumeTasksResponse resp = new DescribeReplaceRootVolumeTasksResponse();
            do
            {
                DescribeReplaceRootVolumeTasksRequest req = new DescribeReplaceRootVolumeTasksRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeReplaceRootVolumeTasks(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ReplaceRootVolumeTasks)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}