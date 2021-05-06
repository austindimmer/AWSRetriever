using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;

namespace CloudOps.Lambda
{
    public class ListLayersOperation : Operation
    {
        public override string Name => "ListLayers";

        public override string Description => "Lists AWS Lambda layers and shows information about the latest version of each. Specify a runtime identifier to list only layers that indicate that they&#39;re compatible with that runtime.";
 
        public override string RequestURI => "/2018-10-31/layers";

        public override string Method => "GET";

        public override string ServiceName => "Lambda";

        public override string ServiceID => "Lambda";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLambdaConfig config = new AmazonLambdaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLambdaClient client = new AmazonLambdaClient(creds, config);
            
            ListLayersResponse resp = new ListLayersResponse();
            do
            {
                ListLayersRequest req = new ListLayersRequest
                {
                    Marker = resp.NextMarker
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = await client.ListLayersAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Layers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}