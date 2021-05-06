using Amazon;
using Amazon.StepFunctions;
using Amazon.StepFunctions.Model;
using Amazon.Runtime;

namespace CloudOps.StepFunctions
{
    public class ListExecutionsOperation : Operation
    {
        public override string Name => "ListExecutions";

        public override string Description => "Lists the executions of a state machine that meet the filtering criteria. Results are sorted by time, with the most recent execution first. If nextToken is returned, there are more results available. The value of nextToken is a unique pagination token for each page. Make the call again using the returned token to retrieve the next page. Keep all other arguments unchanged. Each pagination token expires after 24 hours. Using an expired pagination token will return an HTTP 400 InvalidToken error.  This operation is eventually consistent. The results are best effort and may not reflect very recent updates and changes.  This API action is not supported by EXPRESS state machines.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "StepFunctions";

        public override string ServiceID => "SFN";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonStepFunctionsConfig config = new AmazonStepFunctionsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonStepFunctionsClient client = new AmazonStepFunctionsClient(creds, config);
            
            ListExecutionsResponse resp = new ListExecutionsResponse();
            do
            {
                ListExecutionsRequest req = new ListExecutionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListExecutionsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Executions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}