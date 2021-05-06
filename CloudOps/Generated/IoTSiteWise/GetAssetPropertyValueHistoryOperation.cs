using Amazon;
using Amazon.IoTSiteWise;
using Amazon.IoTSiteWise.Model;
using Amazon.Runtime;

namespace CloudOps.IoTSiteWise
{
    public class GetAssetPropertyValueHistoryOperation : Operation
    {
        public override string Name => "GetAssetPropertyValueHistory";

        public override string Description => "Gets the history of an asset property&#39;s values. For more information, see Querying historical values in the AWS IoT SiteWise User Guide. To identify an asset property, you must specify one of the following:   The assetId and propertyId of an asset property.   A propertyAlias, which is a data stream alias (for example, /company/windfarm/3/turbine/7/temperature). To define an asset property&#39;s alias, see UpdateAssetProperty.  ";
 
        public override string RequestURI => "/properties/history";

        public override string Method => "GET";

        public override string ServiceName => "IoTSiteWise";

        public override string ServiceID => "IoTSiteWise";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTSiteWiseConfig config = new AmazonIoTSiteWiseConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTSiteWiseClient client = new AmazonIoTSiteWiseClient(creds, config);
            
            GetAssetPropertyValueHistoryResponse resp = new GetAssetPropertyValueHistoryResponse();
            do
            {
                GetAssetPropertyValueHistoryRequest req = new GetAssetPropertyValueHistoryRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.GetAssetPropertyValueHistoryAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AssetPropertyValueHistory)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}