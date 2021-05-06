using Amazon;
using Amazon.ResourceGroups;
using Amazon.ResourceGroups.Model;
using Amazon.Runtime;

namespace CloudOps.ResourceGroups
{
    public class SearchResourcesOperation : Operation
    {
        public override string Name => "SearchResources";

        public override string Description => "Returns a list of AWS resource identifiers that matches the specified query. The query uses the same format as a resource query in a CreateGroup or UpdateGroupQuery operation.  Minimum permissions  To run this command, you must have the following permissions:    resource-groups:SearchResources   ";
 
        public override string RequestURI => "/resources/search";

        public override string Method => "POST";

        public override string ServiceName => "ResourceGroups";

        public override string ServiceID => "Resource Groups";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonResourceGroupsConfig config = new AmazonResourceGroupsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonResourceGroupsClient client = new AmazonResourceGroupsClient(creds, config);
            
            SearchResourcesResponse resp = new SearchResourcesResponse();
            do
            {
                try
                {
                    SearchResourcesRequest req = new SearchResourcesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.SearchResourcesAsync(req);
                    
                    foreach (var obj in resp.ResourceIdentifiers)
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