using Amazon;
using Amazon.AWSHealth;
using Amazon.AWSHealth.Model;
using Amazon.Runtime;

namespace CloudOps.AWSHealth
{
    public class DescribeEntityAggregatesOperation : Operation
    {
        public override string Name => "DescribeEntityAggregates";

        public override string Description => "Returns the number of entities that are affected by each of the specified events. If no events are specified, the counts of all affected entities are returned.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "AWSHealth";

        public override string ServiceID => "Health";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAWSHealthConfig config = new AmazonAWSHealthConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAWSHealthClient client = new AmazonAWSHealthClient(creds, config);
            
            DescribeEntityAggregatesResponse resp = new DescribeEntityAggregatesResponse();
            DescribeEntityAggregatesRequest req = new DescribeEntityAggregatesRequest
            {                    
                                    
            };
            resp = await client.DescribeEntityAggregatesAsync(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.EntityAggregates)
            {
                AddObject(obj);
            }
            
        }
    }
}