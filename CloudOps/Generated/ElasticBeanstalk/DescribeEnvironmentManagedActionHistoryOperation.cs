using Amazon;
using Amazon.ElasticBeanstalk;
using Amazon.ElasticBeanstalk.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticBeanstalk
{
    public class DescribeEnvironmentManagedActionHistoryOperation : Operation
    {
        public override string Name => "DescribeEnvironmentManagedActionHistory";

        public override string Description => "Lists an environment&#39;s completed and failed managed actions.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ElasticBeanstalk";

        public override string ServiceID => "Elastic Beanstalk";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticBeanstalkConfig config = new AmazonElasticBeanstalkConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticBeanstalkClient client = new AmazonElasticBeanstalkClient(creds, config);
            
            DescribeEnvironmentManagedActionHistoryResponse resp = new DescribeEnvironmentManagedActionHistoryResponse();
            do
            {
                DescribeEnvironmentManagedActionHistoryRequest req = new DescribeEnvironmentManagedActionHistoryRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = client.DescribeEnvironmentManagedActionHistory(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ManagedActionHistoryItems)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}