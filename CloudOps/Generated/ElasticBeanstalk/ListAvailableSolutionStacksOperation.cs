using Amazon;
using Amazon.ElasticBeanstalk;
using Amazon.ElasticBeanstalk.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticBeanstalk
{
    public class ListAvailableSolutionStacksOperation : Operation
    {
        public override string Name => "ListAvailableSolutionStacks";

        public override string Description => "Returns a list of the available solution stack names, with the public version first and then in reverse chronological order.";
 
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
            
            ListAvailableSolutionStacksResponse resp = new ListAvailableSolutionStacksResponse();
            ListAvailableSolutionStacksRequest req = new ListAvailableSolutionStacksRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.ListAvailableSolutionStacksAsync(req);
                
                foreach (var obj in resp.SolutionStacks)
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