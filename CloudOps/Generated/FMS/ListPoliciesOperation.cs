using Amazon;
using Amazon.FMS;
using Amazon.FMS.Model;
using Amazon.Runtime;

namespace CloudOps.FMS
{
    public class ListPoliciesOperation : Operation
    {
        public override string Name => "ListPolicies";

        public override string Description => "Returns an array of PolicySummary objects.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "FMS";

        public override string ServiceID => "FMS";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonFMSConfig config = new AmazonFMSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonFMSClient client = new AmazonFMSClient(creds, config);
            
            ListPoliciesResponse resp = new ListPoliciesResponse();
            do
            {
                ListPoliciesRequest req = new ListPoliciesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListPoliciesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PolicyList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}