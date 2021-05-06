using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;

namespace CloudOps.Lambda
{
    public class ListEventSourceMappingsOperation : Operation
    {
        public override string Name => "ListEventSourceMappings";

        public override string Description => "Lists event source mappings. Specify an EventSourceArn to only show event source mappings for a single event source.";
 
        public override string RequestURI => "/2015-03-31/event-source-mappings/";

        public override string Method => "GET";

        public override string ServiceName => "Lambda";

        public override string ServiceID => "Lambda";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLambdaConfig config = new AmazonLambdaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLambdaClient client = new AmazonLambdaClient(creds, config);
            
            ListEventSourceMappingsResponse resp = new ListEventSourceMappingsResponse();
            do
            {
                try
                {
                    ListEventSourceMappingsRequest req = new ListEventSourceMappingsRequest
                    {
                        Marker = resp.NextMarker
                        ,
                        MaxItems = maxItems
                                            
                    };

                    resp = await client.ListEventSourceMappingsAsync(req);
                    
                    foreach (var obj in resp.EventSourceMappings)
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
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}