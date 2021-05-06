using Amazon;
using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.Runtime;

namespace CloudOps.CloudFormation
{
    public class ListStackSetsOperation : Operation
    {
        public override string Name => "ListStackSets";

        public override string Description => "Returns summary information about stack sets that are associated with the user.   [Self-managed permissions] If you set the CallAs parameter to SELF while signed in to your AWS account, ListStackSets returns all self-managed stack sets in your AWS account.   [Service-managed permissions] If you set the CallAs parameter to SELF while signed in to the organization&#39;s management account, ListStackSets returns all stack sets in the management account.   [Service-managed permissions] If you set the CallAs parameter to DELEGATED_ADMIN while signed in to your member account, ListStackSets returns all stack sets with service-managed permissions in the management account.  ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudFormation";

        public override string ServiceID => "CloudFormation";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudFormationConfig config = new AmazonCloudFormationConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudFormationClient client = new AmazonCloudFormationClient(creds, config);
            
            ListStackSetsResponse resp = new ListStackSetsResponse();
            do
            {
                try
                {
                    ListStackSetsRequest req = new ListStackSetsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListStackSetsAsync(req);
                    
                    foreach (var obj in resp.Summaries)
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