using Amazon;
using Amazon.MediaPackageVod;
using Amazon.MediaPackageVod.Model;
using Amazon.Runtime;

namespace CloudOps.MediaPackageVod
{
    public class ListPackagingConfigurationsOperation : Operation
    {
        public override string Name => "ListPackagingConfigurations";

        public override string Description => "Returns a collection of MediaPackage VOD PackagingConfiguration resources.";
 
        public override string RequestURI => "/packaging_configurations";

        public override string Method => "GET";

        public override string ServiceName => "MediaPackageVod";

        public override string ServiceID => "MediaPackage Vod";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaPackageVodConfig config = new AmazonMediaPackageVodConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaPackageVodClient client = new AmazonMediaPackageVodClient(creds, config);
            
            ListPackagingConfigurationsResponse resp = new ListPackagingConfigurationsResponse();
            do
            {
                try
                {
                    ListPackagingConfigurationsRequest req = new ListPackagingConfigurationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListPackagingConfigurationsAsync(req);
                    
                    foreach (var obj in resp.PackagingConfigurations)
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