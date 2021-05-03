using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;

namespace CloudOps.Lambda
{
    public class ListFunctionsOperation : Operation
    {
        public override string Name => "ListFunctions";

        public override string Description => "Returns a list of Lambda functions, with the version-specific configuration of each. Lambda returns up to 50 functions per call. Set FunctionVersion to ALL to include all published versions of each function in addition to the unpublished version.   The ListFunctions action returns a subset of the FunctionConfiguration fields. To get the additional fields (State, StateReasonCode, StateReason, LastUpdateStatus, LastUpdateStatusReason, LastUpdateStatusReasonCode) for a function or version, use GetFunction. ";
 
        public override string RequestURI => "/2015-03-31/functions/";

        public override string Method => "GET";

        public override string ServiceName => "Lambda";

        public override string ServiceID => "Lambda";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLambdaConfig config = new AmazonLambdaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLambdaClient client = new AmazonLambdaClient(creds, config);
            
            ListFunctionsResponse resp = new ListFunctionsResponse();
            do
            {
                ListFunctionsRequest req = new ListFunctionsRequest
                {
                    Marker = resp.NextMarker
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = client.ListFunctions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Functions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}