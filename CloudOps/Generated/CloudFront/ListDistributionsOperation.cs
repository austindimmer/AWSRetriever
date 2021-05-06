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

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudFrontConfig config = new AmazonCloudFrontConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudFrontClient client = new AmazonCloudFrontClient(creds, config);
            
            ListDistributionsResponse resp = new ListDistributionsResponse();
            do
            {
                try
                {
                    ListDistributionsRequest req = new ListDistributionsRequest
                    {
                        Marker = resp.DistributionList.NextMarker
                        ,
                        MaxItems = maxItems.ToString()
                                            
                    };

                    resp = await client.ListDistributionsAsync(req);
                    
                    foreach (var obj in resp.DistributionList.Items)
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
            while (!string.IsNullOrEmpty(resp.DistributionList.NextMarker));
        }
    }
}