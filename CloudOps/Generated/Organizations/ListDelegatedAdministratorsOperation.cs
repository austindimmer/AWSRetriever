using Amazon;
using Amazon.Organizations;
using Amazon.Organizations.Model;
using Amazon.Runtime;

namespace CloudOps.Organizations
{
    public class ListDelegatedAdministratorsOperation : Operation
    {
        public override string Name => "ListDelegatedAdministrators";

        public override string Description => "Lists the AWS accounts that are designated as delegated administrators in this organization. This operation can be called only from the organization&#39;s management account or by a member account that is a delegated administrator for an AWS service.";
 
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
            
            ListDelegatedAdministratorsResponse resp = new ListDelegatedAdministratorsResponse();
            do
            {
                ListDelegatedAdministratorsRequest req = new ListDelegatedAdministratorsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListDelegatedAdministrators(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DelegatedAdministrators)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}