using Amazon;
using Amazon.MediaStoreData;
using Amazon.MediaStoreData.Model;
using Amazon.Runtime;

namespace CloudOps.MediaStoreData
{
    public class ListItemsOperation : Operation
    {
        public override string Name => "ListItems";

        public override string Description => "Provides a list of metadata entries about folders and objects in the specified folder.";
 
        public override string RequestURI => "/";

        public override string Method => "GET";

        public override string ServiceName => "MediaStoreData";

        public override string ServiceID => "MediaStore Data";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaStoreDataConfig config = new AmazonMediaStoreDataConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaStoreDataClient client = new AmazonMediaStoreDataClient(creds, config);
            
            ListItemsResponse resp = new ListItemsResponse();
            do
            {
                ListItemsRequest req = new ListItemsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListItems(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Items)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}