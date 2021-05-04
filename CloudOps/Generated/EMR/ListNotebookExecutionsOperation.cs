using Amazon;
using Amazon.ElasticMapReduce;
using Amazon.ElasticMapReduce.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticMapReduce
{
    public class ListNotebookExecutionsOperation : Operation
    {
        public override string Name => "ListNotebookExecutions";

        public override string Description => "Provides summaries of all notebook executions. You can filter the list based on multiple criteria such as status, time range, and editor id. Returns a maximum of 50 notebook executions and a marker to track the paging of a longer notebook execution list across multiple ListNotebookExecution calls.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ElasticMapReduce";

        public override string ServiceID => "ElasticMapReduce";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticMapReduceConfig config = new AmazonElasticMapReduceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);
            AmazonElasticMapReduceClient client = new AmazonElasticMapReduceClient(creds, config);
            
            ListNotebookExecutionsResponse resp = new ListNotebookExecutionsResponse();
            do
            {
                ListNotebookExecutionsRequest req = new ListNotebookExecutionsRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = client.ListNotebookExecutions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.NotebookExecutions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}