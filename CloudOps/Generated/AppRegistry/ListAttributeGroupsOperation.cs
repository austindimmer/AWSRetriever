using Amazon;
using Amazon.AppRegistry;
using Amazon.AppRegistry.Model;
using Amazon.Runtime;

namespace CloudOps.AppRegistry
{
    public class ListAttributeGroupsOperation : Operation
    {
        public override string Name => "ListAttributeGroups";

        public override string Description => "Lists all attribute groups which you have access to. Results are paginated.";
 
        public override string RequestURI => "/attribute-groups";

        public override string Method => "GET";

        public override string ServiceName => "AppRegistry";

        public override string ServiceID => "Service Catalog AppRegistry";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppRegistryConfig config = new AmazonAppRegistryConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppRegistryClient client = new AmazonAppRegistryClient(creds, config);
            
            ListAttributeGroupsResponse resp = new ListAttributeGroupsResponse();
            do
            {
                try
                {
                    ListAttributeGroupsRequest req = new ListAttributeGroupsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListAttributeGroupsAsync(req);
                    
                    foreach (var obj in resp.AttributeGroups)
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