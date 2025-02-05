using Amazon;
using Amazon.FMS;
using Amazon.FMS.Model;
using Amazon.Runtime;

namespace CloudOps.FMS
{
    public class ListMemberAccountsOperation : Operation
    {
        public override string Name => "ListMemberAccounts";

        public override string Description => "Returns a MemberAccounts object that lists the member accounts in the administrator&#39;s AWS organization. The ListMemberAccounts must be submitted by the account that is set as the AWS Firewall Manager administrator.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "FMS";

        public override string ServiceID => "FMS";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonFMSConfig config = new AmazonFMSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonFMSClient client = new AmazonFMSClient(creds, config);
            
            ListMemberAccountsResponse resp = new ListMemberAccountsResponse();
            do
            {
                ListMemberAccountsRequest req = new ListMemberAccountsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListMemberAccounts(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.MemberAccounts)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}