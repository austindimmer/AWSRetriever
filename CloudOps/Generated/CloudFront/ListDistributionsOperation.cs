using Amazon;
using Amazon.CloudFront;
using Amazon.CloudFront.Model;
using Amazon.Runtime;

namespace CloudOps.CloudFront
{
    public class ListDistributionsOperation : Operation
    {
        public override string Name => "ListDistributions";

        public override string Description => "List CloudFront distributions.";
 
        public override string RequestURI => "/2020-05-31/distribution";

        public override string Method => "GET";

        public override string ServiceName => "CloudFront";

        public override string ServiceID => "CloudFront";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudFrontConfig config = new AmazonCloudFrontConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudFrontClient client = new AmazonCloudFrontClient(creds, config);
            
            ListDistributionsResponse resp = new ListDistributionsResponse();
            do
            {
                ListDistributionsRequest req = new ListDistributionsRequest
                {
                    Marker = resp.DistributionListNextMarker
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = client.ListDistributions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DistributionListItems)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.DistributionListNextMarker));
        }
    }
}