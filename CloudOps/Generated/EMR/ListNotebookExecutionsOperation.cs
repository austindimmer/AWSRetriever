using Amazon;
using Amazon.EMR;
using Amazon.EMR.Model;
using Amazon.Runtime;

namespace CloudOps.EMR
{
    public class ListNotebookExecutionsOperation : Operation
    {
        public override string Name => "ListNotebookExecutions";

        public override string Description => "Provides summaries of all notebook executions. You can filter the list based on multiple criteria such as status, time range, and editor id. Returns a maximum of 50 notebook executions and a marker to track the paging of a longer notebook execution list across multiple ListNotebookExecution calls.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EMR";

        public override string ServiceID => "EMR";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEMRConfig config = new AmazonEMRConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEMRClient client = new AmazonEMRClient(creds, config);
            
            ListNotebookExecutionsResponse resp = new ListNotebookExecutionsResponse();
            do
            {
                try
                {
                    ListNotebookExecutionsRequest req = new ListNotebookExecutionsRequest
                    {
                        Marker = resp.Marker
                                            
                    };

                    resp = await client.ListNotebookExecutionsAsync(req);
                    
                    foreach (var obj in resp.NotebookExecutions)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}