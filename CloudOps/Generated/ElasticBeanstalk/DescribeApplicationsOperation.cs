using Amazon;
using Amazon.ElasticBeanstalk;
using Amazon.ElasticBeanstalk.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticBeanstalk
{
    public class DescribeApplicationsOperation : Operation
    {
        public override string Name => "DescribeApplications";

        public override string Description => "Returns the descriptions of existing applications.";
 
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
            
            DescribeApplicationsResponse resp = new DescribeApplicationsResponse();
            DescribeApplicationsRequest req = new DescribeApplicationsRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribeApplicationsAsync(req);
                
                foreach (var obj in resp.Applications)
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