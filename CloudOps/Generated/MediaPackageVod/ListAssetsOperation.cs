using Amazon;
using Amazon.MediaPackageVod;
using Amazon.MediaPackageVod.Model;
using Amazon.Runtime;

namespace CloudOps.MediaPackageVod
{
    public class ListAssetsOperation : Operation
    {
        public override string Name => "ListAssets";

        public override string Description => "Returns a collection of MediaPackage VOD Asset resources.";
 
        public override string RequestURI => "/assets";

        public override string Method => "GET";

        public override string ServiceName => "MediaPackageVod";

        public override string ServiceID => "MediaPackage Vod";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaPackageVodConfig config = new AmazonMediaPackageVodConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaPackageVodClient client = new AmazonMediaPackageVodClient(creds, config);
            
            ListAssetsResponse resp = new ListAssetsResponse();
            do
            {
                try
                {
                    ListAssetsRequest req = new ListAssetsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListAssetsAsync(req);
                    
                    foreach (var obj in resp.Assets)
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