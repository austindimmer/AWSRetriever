using Amazon;
using Amazon.Organizations;
using Amazon.Organizations.Model;
using Amazon.Runtime;

namespace CloudOps.Organizations
{
    public class ListAWSServiceAccessForOrganizationOperation : Operation
    {
        public override string Name => "ListAWSServiceAccessForOrganization";

        public override string Description => "Returns a list of the AWS services that you enabled to integrate with your organization. After a service on this list creates the resources that it requires for the integration, it can perform operations on your organization and its accounts. For more information about integrating other services with AWS Organizations, including the list of services that currently work with Organizations, see Integrating AWS Organizations with Other AWS Services in the AWS Organizations User Guide.  This operation can be called only from the organization&#39;s management account or by a member account that is a delegated administrator for an AWS service.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Organizations";

        public override string ServiceID => "Organizations";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonOrganizationsConfig config = new AmazonOrganizationsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonOrganizationsClient client = new AmazonOrganizationsClient(creds, config);
            
            ListAWSServiceAccessForOrganizationResponse resp = new ListAWSServiceAccessForOrganizationResponse();
            do
            {
                try
                {
                    ListAWSServiceAccessForOrganizationRequest req = new ListAWSServiceAccessForOrganizationRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListAWSServiceAccessForOrganizationAsync(req);
                    
                    foreach (var obj in resp.EnabledServicePrincipals)
                    {
                        AddObject(obj);
                    }
                    
                }
                catch (System.Exception)
                {
                    CheckError(resp.HttpStatusCode, "200");                
                    throw;
                }

            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}