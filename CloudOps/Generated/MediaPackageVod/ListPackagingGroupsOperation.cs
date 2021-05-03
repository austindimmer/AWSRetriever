using Amazon;
using Amazon.MediaPackageVod;
using Amazon.MediaPackageVod.Model;
using Amazon.Runtime;

namespace CloudOps.MediaPackageVod
{
    public class ListPackagingGroupsOperation : Operation
    {
        public override string Name => "ListPackagingGroups";

        public override string Description => "Returns a collection of MediaPackage VOD PackagingGroup resources.";
 
        public override string RequestURI => "/packaging_groups";

        public override string Method => "GET";

        public override string ServiceName => "MediaPackageVod";

        public override string ServiceID => "MediaPackage Vod";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaPackageVodConfig config = new AmazonMediaPackageVodConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaPackageVodClient client = new AmazonMediaPackageVodClient(creds, config);
            
            ListPackagingGroupsResponse resp = new ListPackagingGroupsResponse();
            do
            {
                ListPackagingGroupsRequest req = new ListPackagingGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListPackagingGroups(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PackagingGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}