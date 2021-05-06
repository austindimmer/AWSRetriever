using Amazon;
using Amazon.MediaPackage;
using Amazon.MediaPackage.Model;
using Amazon.Runtime;

namespace CloudOps.MediaPackage
{
    public class ListOriginEndpointsOperation : Operation
    {
        public override string Name => "ListOriginEndpoints";

        public override string Description => "Returns a collection of OriginEndpoint records.";
 
        public override string RequestURI => "/origin_endpoints";

        public override string Method => "GET";

        public override string ServiceName => "MediaPackage";

        public override string ServiceID => "MediaPackage";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaPackageConfig config = new AmazonMediaPackageConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaPackageClient client = new AmazonMediaPackageClient(creds, config);
            
            ListOriginEndpointsResponse resp = new ListOriginEndpointsResponse();
            do
            {
                try
                {
                    ListOriginEndpointsRequest req = new ListOriginEndpointsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListOriginEndpointsAsync(req);
                    
                    foreach (var obj in resp.OriginEndpoints)
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
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}