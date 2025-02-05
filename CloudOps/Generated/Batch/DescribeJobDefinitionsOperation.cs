using Amazon;
using Amazon.Batch;
using Amazon.Batch.Model;
using Amazon.Runtime;

namespace CloudOps.Batch
{
    public class DescribeJobDefinitionsOperation : Operation
    {
        public override string Name => "DescribeJobDefinitions";

        public override string Description => "Describes a list of job definitions. You can specify a status (such as ACTIVE) to only return job definitions that match that status.";
 
        public override string RequestURI => "/v1/describejobdefinitions";

        public override string Method => "POST";

        public override string ServiceName => "Batch";

        public override string ServiceID => "Batch";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBatchConfig config = new AmazonBatchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBatchClient client = new AmazonBatchClient(creds, config);
            
            DescribeJobDefinitionsResponse resp = new DescribeJobDefinitionsResponse();
            do
            {
                DescribeJobDefinitionsRequest req = new DescribeJobDefinitionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeJobDefinitions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.JobDefinitions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}