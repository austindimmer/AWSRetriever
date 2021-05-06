using Amazon;
using Amazon.Transfer;
using Amazon.Transfer.Model;
using Amazon.Runtime;

namespace CloudOps.Transfer
{
    public class ListSecurityPoliciesOperation : Operation
    {
        public override string Name => "ListSecurityPolicies";

        public override string Description => "Lists the security policies that are attached to your file transfer protocol-enabled servers.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Transfer";

        public override string ServiceID => "Transfer";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonTransferConfig config = new AmazonTransferConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonTransferClient client = new AmazonTransferClient(creds, config);
            
            ListSecurityPoliciesResponse resp = new ListSecurityPoliciesResponse();
            do
            {
                ListSecurityPoliciesRequest req = new ListSecurityPoliciesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListSecurityPoliciesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SecurityPolicyNames)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}