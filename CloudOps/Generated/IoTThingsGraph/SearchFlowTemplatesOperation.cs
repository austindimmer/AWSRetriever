using Amazon;
using Amazon.IoTThingsGraph;
using Amazon.IoTThingsGraph.Model;
using Amazon.Runtime;

namespace CloudOps.IoTThingsGraph
{
    public class SearchFlowTemplatesOperation : Operation
    {
        public override string Name => "SearchFlowTemplates";

        public override string Description => "Searches for summary information about workflows.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "IoTThingsGraph";

        public override string ServiceID => "IoTThingsGraph";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTThingsGraphConfig config = new AmazonIoTThingsGraphConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTThingsGraphClient client = new AmazonIoTThingsGraphClient(creds, config);
            
            SearchFlowTemplatesResponse resp = new SearchFlowTemplatesResponse();
            do
            {
                SearchFlowTemplatesRequest req = new SearchFlowTemplatesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.SearchFlowTemplatesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Summaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}