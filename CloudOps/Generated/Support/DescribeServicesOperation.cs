using Amazon;
using Amazon.AWSSupport;
using Amazon.AWSSupport.Model;
using Amazon.Runtime;

namespace CloudOps.Support
{
    public class DescribeServicesOperation : Operation
    {
        public override string Name => "DescribeServices";

        public override string Description => "Returns the current list of AWS services and a list of service categories for each service. You then use service names and categories in your CreateCase requests. Each AWS service has its own set of categories. The service codes and category codes correspond to the values that appear in the Service and Category lists on the AWS Support Center Create Case page. The values in those fields don&#39;t necessarily match the service codes and categories returned by the DescribeServices operation. Always use the service codes and categories that the DescribeServices operation returns, so that you have the most recent set of service and category codes.    You must have a Business or Enterprise support plan to use the AWS Support API.    If you call the AWS Support API from an account that does not have a Business or Enterprise support plan, the SubscriptionRequiredException error message appears. For information about changing your support plan, see AWS Support.   ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Support";

        public override string ServiceID => "Support";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAWSSupportConfig config = new AmazonAWSSupportConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAWSSupportClient client = new AmazonAWSSupportClient(creds, config);
            
            DescribeServicesResponse resp = new DescribeServicesResponse();
            DescribeServicesRequest req = new DescribeServicesRequest
            {                    
                                    
            };
            resp = await client.DescribeServicesAsync(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.Services)
            {
                AddObject(obj);
            }
            
        }
    }
}