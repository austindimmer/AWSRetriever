using Amazon;
using Amazon.Organizations;
using Amazon.Organizations.Model;
using Amazon.Runtime;

namespace CloudOps.Organizations
{
    public class ListAccountsOperation : Operation
    {
        public override string Name => "ListAccounts";

        public override string Description => "Lists all the accounts in the organization. To request only the accounts in a specified root or organizational unit (OU), use the ListAccountsForParent operation instead.  Always check the NextToken response parameter for a null value when calling a List* operation. These operations can occasionally return an empty set of results even when there are more results available. The NextToken response parameter value is null only when there are no more results to display.  This operation can be called only from the organization&#39;s management account or by a member account that is a delegated administrator for an AWS service.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Organizations";

        public override string ServiceID => "Organizations";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonOrganizationsConfig config = new AmazonOrganizationsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonOrganizationsClient client = new AmazonOrganizationsClient(creds, config);
            
            ListAccountsResponse resp = new ListAccountsResponse();
            do
            {
                ListAccountsRequest req = new ListAccountsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListAccounts(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Accounts)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}