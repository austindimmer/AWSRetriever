using Amazon;
using Amazon.ResourceGroupsTaggingAPI;
using Amazon.ResourceGroupsTaggingAPI.Model;
using Amazon.Runtime;

namespace CloudOps.ResourceGroupsTaggingAPI
{
    public class GetResourcesOperation : Operation
    {
        public override string Name => "GetResources";

        public override string Description => "Returns all the tagged or previously tagged resources that are located in the specified Region for the AWS account. Depending on what information you want returned, you can also specify the following:    Filters that specify what tags and resource types you want returned. The response includes all tags that are associated with the requested resources.   Information about compliance with the account&#39;s effective tag policy. For more information on tag policies, see Tag Policies in the AWS Organizations User Guide.    This operation supports pagination, where the response can be sent in multiple pages. You should check the PaginationToken response parameter to determine if there are additional results available to return. Repeat the query, passing the PaginationToken response parameter value as an input to the next request until you recieve a null value. A null value for PaginationToken indicates that there are no more results waiting to be returned.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ResourceGroupsTaggingAPI";

        public override string ServiceID => "Resource Groups Tagging API";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonResourceGroupsTaggingAPIConfig config = new AmazonResourceGroupsTaggingAPIConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonResourceGroupsTaggingAPIClient client = new AmazonResourceGroupsTaggingAPIClient(creds, config);
            
            GetResourcesResponse resp = new GetResourcesResponse();
            do
            {
                GetResourcesRequest req = new GetResourcesRequest
                {
                    PaginationToken = resp.PaginationToken
                    ,
                    ResourcesPerPage = maxItems
                                        
                };

                resp = client.GetResources(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ResourceTagMappingList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.PaginationToken));
        }
    }
}