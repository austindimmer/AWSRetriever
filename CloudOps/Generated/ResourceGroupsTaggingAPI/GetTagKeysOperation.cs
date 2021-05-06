using Amazon;
using Amazon.ResourceGroupsTaggingAPI;
using Amazon.ResourceGroupsTaggingAPI.Model;
using Amazon.Runtime;

namespace CloudOps.ResourceGroupsTaggingAPI
{
    public class GetTagKeysOperation : Operation
    {
        public override string Name => "GetTagKeys";

        public override string Description => "Returns all tag keys currently in use in the specified Region for the calling AWS account. This operation supports pagination, where the response can be sent in multiple pages. You should check the PaginationToken response parameter to determine if there are additional results available to return. Repeat the query, passing the PaginationToken response parameter value as an input to the next request until you recieve a null value. A null value for PaginationToken indicates that there are no more results waiting to be returned.";
 
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
            
            GetTagKeysResponse resp = new GetTagKeysResponse();
            do
            {
                try
                {
                    GetTagKeysRequest req = new GetTagKeysRequest
                    {
                        PaginationToken = resp.PaginationToken
                                            
                    };

                    resp = await client.GetTagKeysAsync(req);
                    
                    foreach (var obj in resp.TagKeys)
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
            while (!string.IsNullOrEmpty(resp.PaginationToken));
        }
    }
}