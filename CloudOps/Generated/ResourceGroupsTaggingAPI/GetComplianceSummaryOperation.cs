using Amazon;
using Amazon.ResourceGroupsTaggingAPI;
using Amazon.ResourceGroupsTaggingAPI.Model;
using Amazon.Runtime;

namespace CloudOps.ResourceGroupsTaggingAPI
{
    public class GetComplianceSummaryOperation : Operation
    {
        public override string Name => "GetComplianceSummary";

        public override string Description => "Returns a table that shows counts of resources that are noncompliant with their tag policies. For more information on tag policies, see Tag Policies in the AWS Organizations User Guide.  You can call this operation only from the organization&#39;s management account and from the us-east-1 Region. This operation supports pagination, where the response can be sent in multiple pages. You should check the PaginationToken response parameter to determine if there are additional results available to return. Repeat the query, passing the PaginationToken response parameter value as an input to the next request until you recieve a null value. A null value for PaginationToken indicates that there are no more results waiting to be returned.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ResourceGroupsTaggingAPI";

        public override string ServiceID => "Resource Groups Tagging API";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonResourceGroupsTaggingAPIConfig config = new AmazonResourceGroupsTaggingAPIConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonResourceGroupsTaggingAPIClient client = new AmazonResourceGroupsTaggingAPIClient(creds, config);
            
            GetComplianceSummaryResponse resp = new GetComplianceSummaryResponse();
            do
            {
                GetComplianceSummaryRequest req = new GetComplianceSummaryRequest
                {
                    PaginationToken = resp.PaginationToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.GetComplianceSummaryAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.PaginationToken));
        }
    }
}