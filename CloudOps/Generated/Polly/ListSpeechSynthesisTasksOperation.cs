using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;

namespace CloudOps.Polly
{
    public class ListSpeechSynthesisTasksOperation : Operation
    {
        public override string Name => "ListSpeechSynthesisTasks";

        public override string Description => "Returns a list of SpeechSynthesisTask objects ordered by their creation date. This operation can filter the tasks by their status, for example, allowing users to list only tasks that are completed.";
 
        public override string RequestURI => "/v1/synthesisTasks";

        public override string Method => "GET";

        public override string ServiceName => "Polly";

        public override string ServiceID => "Polly";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPollyConfig config = new AmazonPollyConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPollyClient client = new AmazonPollyClient(creds, config);
            
            ListSpeechSynthesisTasksResponse resp = new ListSpeechSynthesisTasksResponse();
            do
            {
                try
                {
                    ListSpeechSynthesisTasksRequest req = new ListSpeechSynthesisTasksRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListSpeechSynthesisTasksAsync(req);
                    
                    foreach (var obj in resp.SynthesisTasks)
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
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}