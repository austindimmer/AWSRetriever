using Amazon;
using Amazon.WorkMail;
using Amazon.WorkMail.Model;
using Amazon.Runtime;

namespace CloudOps.WorkMail
{
    public class ListOrganizationsOperation : Operation
    {
        public override string Name => "ListOrganizations";

        public override string Description => "Returns summaries of the customer&#39;s organizations.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "WorkMail";

        public override string ServiceID => "WorkMail";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWorkMailConfig config = new AmazonWorkMailConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWorkMailClient client = new AmazonWorkMailClient(creds, config);
            
            ListOrganizationsResponse resp = new ListOrganizationsResponse();
            do
            {
                ListOrganizationsRequest req = new ListOrganizationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListOrganizations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.OrganizationSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}