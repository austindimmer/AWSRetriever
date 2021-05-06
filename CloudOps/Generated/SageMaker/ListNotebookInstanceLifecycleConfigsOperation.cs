using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListNotebookInstanceLifecycleConfigsOperation : Operation
    {
        public override string Name => "ListNotebookInstanceLifecycleConfigs";

        public override string Description => "Lists notebook instance lifestyle configurations created with the CreateNotebookInstanceLifecycleConfig API.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SageMaker";

        public override string ServiceID => "SageMaker";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSageMakerConfig config = new AmazonSageMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSageMakerClient client = new AmazonSageMakerClient(creds, config);
            
            ListNotebookInstanceLifecycleConfigsResponse resp = new ListNotebookInstanceLifecycleConfigsResponse();
            do
            {
                ListNotebookInstanceLifecycleConfigsRequest req = new ListNotebookInstanceLifecycleConfigsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListNotebookInstanceLifecycleConfigsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.NotebookInstanceLifecycleConfigs)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}