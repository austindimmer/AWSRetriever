using Amazon;
using Amazon.IoTThingsGraph;
using Amazon.IoTThingsGraph.Model;
using Amazon.Runtime;

namespace CloudOps.IoTThingsGraph
{
    public class SearchSystemTemplatesOperation : Operation
    {
        public override string Name => "SearchSystemTemplates";

        public override string Description => "Searches for summary information about systems in the user&#39;s account. You can filter by the ID of a workflow to return only systems that use the specified workflow.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "IoTThingsGraph";

        public override string ServiceID => "IoTThingsGraph";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTThingsGraphConfig config = new AmazonIoTThingsGraphConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTThingsGraphClient client = new AmazonIoTThingsGraphClient(creds, config);
            
            SearchSystemTemplatesResponse resp = new SearchSystemTemplatesResponse();
            do
            {
                SearchSystemTemplatesRequest req = new SearchSystemTemplatesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.SearchSystemTemplates(req);
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