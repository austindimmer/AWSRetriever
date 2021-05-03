using Amazon;
using Amazon.Pricing;
using Amazon.Pricing.Model;
using Amazon.Runtime;

namespace CloudOps.Pricing
{
    public class DescribeServicesOperation : Operation
    {
        public override string Name => "DescribeServices";

        public override string Description => "Returns the metadata for one service or a list of the metadata for all services. Use this without a service code to get the service codes for all services. Use it with a service code, such as AmazonEC2, to get information specific to that service, such as the attribute names available for that service. For example, some of the attribute names available for EC2 are volumeType, maxIopsVolume, operation, locationType, and instanceCapacity10xlarge.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Pricing";

        public override string ServiceID => "Pricing";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPricingConfig config = new AmazonPricingConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPricingClient client = new AmazonPricingClient(creds, config);
            
            DescribeServicesResponse resp = new DescribeServicesResponse();
            do
            {
                DescribeServicesRequest req = new DescribeServicesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeServices(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Services)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.FormatVersion)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}