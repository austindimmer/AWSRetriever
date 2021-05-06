using Amazon;
using Amazon.SSOAdmin;
using Amazon.SSOAdmin.Model;
using Amazon.Runtime;

namespace CloudOps.SSOAdmin
{
    public class ListInstancesOperation : Operation
    {
        public override string Name => "ListInstances";

        public override string Description => "Lists the SSO instances that the caller has access to.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SSOAdmin";

        public override string ServiceID => "SSO Admin";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSSOAdminConfig config = new AmazonSSOAdminConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSSOAdminClient client = new AmazonSSOAdminClient(creds, config);
            
            ListInstancesResponse resp = new ListInstancesResponse();
            do
            {
                ListInstancesRequest req = new ListInstancesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListInstancesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Instances)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}