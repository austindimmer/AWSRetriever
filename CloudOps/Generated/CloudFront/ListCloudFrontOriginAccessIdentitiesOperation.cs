using Amazon;
using Amazon.CloudFront;
using Amazon.CloudFront.Model;
using Amazon.Runtime;

namespace CloudOps.CloudFront
{
    public class ListCloudFrontOriginAccessIdentitiesOperation : Operation
    {
        public override string Name => "ListCloudFrontOriginAccessIdentities";

        public override string Description => "Lists origin access identities.";
 
        public override string RequestURI => "/2020-05-31/origin-access-identity/cloudfront";

        public override string Method => "GET";

        public override string ServiceName => "CloudFront";

        public override string ServiceID => "CloudFront";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudFrontConfig config = new AmazonCloudFrontConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudFrontClient client = new AmazonCloudFrontClient(creds, config);
            
            ListCloudFrontOriginAccessIdentitiesResponse resp = new ListCloudFrontOriginAccessIdentitiesResponse();
            do
            {
                ListCloudFrontOriginAccessIdentitiesRequest req = new ListCloudFrontOriginAccessIdentitiesRequest
                {
                    Marker = resp.CloudFrontOriginAccessIdentityListNextMarker
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = client.ListCloudFrontOriginAccessIdentities(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CloudFrontOriginAccessIdentityListItems)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.CloudFrontOriginAccessIdentityListNextMarker));
        }
    }
}