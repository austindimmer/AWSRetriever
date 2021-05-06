using Amazon;
using Amazon.IoTThingsGraph;
using Amazon.IoTThingsGraph.Model;
using Amazon.Runtime;

namespace CloudOps.IoTThingsGraph
{
    public class SearchSystemInstancesOperation : Operation
    {
        public override string Name => "SearchSystemInstances";

        public override string Description => "Searches for system instances in the user&#39;s account.";
 
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
            
            SearchSystemInstancesResponse resp = new SearchSystemInstancesResponse();
            do
            {
                SearchSystemInstancesRequest req = new SearchSystemInstancesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.SearchSystemInstancesAsync(req);
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