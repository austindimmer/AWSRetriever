using Amazon;
using Amazon.IoTSiteWise;
using Amazon.IoTSiteWise.Model;
using Amazon.Runtime;

namespace CloudOps.IoTSiteWise
{
    public class ListAccessPoliciesOperation : Operation
    {
        public override string Name => "ListAccessPolicies";

        public override string Description => "Retrieves a paginated list of access policies for an identity (an AWS SSO user, an AWS SSO group, or an IAM user) or an AWS IoT SiteWise Monitor resource (a portal or project).";
 
        public override string RequestURI => "/access-policies";

        public override string Method => "GET";

        public override string ServiceName => "IoTSiteWise";

        public override string ServiceID => "IoTSiteWise";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTSiteWiseConfig config = new AmazonIoTSiteWiseConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTSiteWiseClient client = new AmazonIoTSiteWiseClient(creds, config);
            
            ListAccessPoliciesResponse resp = new ListAccessPoliciesResponse();
            do
            {
                ListAccessPoliciesRequest req = new ListAccessPoliciesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListAccessPolicies(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AccessPolicySummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}