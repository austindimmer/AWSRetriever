using Amazon;
using Amazon.CloudFront;
using Amazon.CloudFront.Model;
using Amazon.Runtime;

namespace CloudOps.CloudFront
{
    public class ListStreamingDistributionsOperation : Operation
    {
        public override string Name => "ListStreamingDistributions";

        public override string Description => "List streaming distributions. ";
 
        public override string RequestURI => "/2020-05-31/streaming-distribution";

        public override string Method => "GET";

        public override string ServiceName => "CloudFront";

        public override string ServiceID => "CloudFront";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudFrontConfig config = new AmazonCloudFrontConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudFrontClient client = new AmazonCloudFrontClient(creds, config);
            
            ListStreamingDistributionsResponse resp = new ListStreamingDistributionsResponse();
            do
            {
                ListStreamingDistributionsRequest req = new ListStreamingDistributionsRequest
                {
                    Marker = resp.StreamingDistributionList.NextMarker,
                    MaxItems = maxItems.ToString()
                };

                resp = await client.ListStreamingDistributionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.StreamingDistributionList.Items)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.StreamingDistributionList.NextMarker));
        }
    }
}