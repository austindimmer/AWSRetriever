using Amazon;
using Amazon.ElasticBeanstalk;
using Amazon.ElasticBeanstalk.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticBeanstalk
{
    public class DescribeEnvironmentsOperation : Operation
    {
        public override string Name => "DescribeEnvironments";

        public override string Description => "Returns descriptions for existing environments.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ElasticBeanstalk";

        public override string ServiceID => "Elastic Beanstalk";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticBeanstalkConfig config = new AmazonElasticBeanstalkConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticBeanstalkClient client = new AmazonElasticBeanstalkClient(creds, config);
            
            DescribeEnvironmentsResponse resp = new DescribeEnvironmentsResponse();
            DescribeEnvironmentsRequest req = new DescribeEnvironmentsRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribeEnvironmentsAsync(req);
                
                foreach (var obj in resp.Environments)
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