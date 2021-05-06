using Amazon;
using Amazon.CloudTrail;
using Amazon.CloudTrail.Model;
using Amazon.Runtime;

namespace CloudOps.CloudTrail
{
    public class DescribeTrailsOperation : Operation
    {
        public override string Name => "DescribeTrails";

        public override string Description => "Retrieves settings for one or more trails associated with the current region for your account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudTrail";

        public override string ServiceID => "CloudTrail";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudTrailConfig config = new AmazonCloudTrailConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudTrailClient client = new AmazonCloudTrailClient(creds, config);
            
            DescribeTrailsResponse resp = new DescribeTrailsResponse();
            DescribeTrailsRequest req = new DescribeTrailsRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribeTrailsAsync(req);
                
                foreach (var obj in resp.TrailList)
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