using Amazon;
using Amazon.FraudDetector;
using Amazon.FraudDetector.Model;
using Amazon.Runtime;

namespace CloudOps.FraudDetector
{
    public class GetVariablesOperation : Operation
    {
        public override string Name => "GetVariables";

        public override string Description => "Gets all of the variables or the specific variable. This is a paginated API. Providing null maxSizePerPage results in retrieving maximum of 100 records per page. If you provide maxSizePerPage the value must be between 50 and 100. To get the next page result, a provide a pagination token from GetVariablesResult as part of your request. Null pagination token fetches the records from the beginning. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "FraudDetector";

        public override string ServiceID => "FraudDetector";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonFraudDetectorConfig config = new AmazonFraudDetectorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonFraudDetectorClient client = new AmazonFraudDetectorClient(creds, config);
            
            GetVariablesResponse resp = new GetVariablesResponse();
            do
            {
                GetVariablesRequest req = new GetVariablesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.GetVariables(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Variables)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}