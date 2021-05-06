using Amazon;
using Amazon.IoTSiteWise;
using Amazon.IoTSiteWise.Model;
using Amazon.Runtime;

namespace CloudOps.IoTSiteWise
{
    public class ListAssetModelsOperation : Operation
    {
        public override string Name => "ListAssetModels";

        public override string Description => "Retrieves a paginated list of summaries of all asset models.";
 
        public override string RequestURI => "/asset-models";

        public override string Method => "GET";

        public override string ServiceName => "IoTSiteWise";

        public override string ServiceID => "IoTSiteWise";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTSiteWiseConfig config = new AmazonIoTSiteWiseConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTSiteWiseClient client = new AmazonIoTSiteWiseClient(creds, config);
            
            ListAssetModelsResponse resp = new ListAssetModelsResponse();
            do
            {
                ListAssetModelsRequest req = new ListAssetModelsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListAssetModelsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AssetModelSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}