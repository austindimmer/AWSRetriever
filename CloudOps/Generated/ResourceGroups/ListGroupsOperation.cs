using Amazon;
using Amazon.ResourceGroups;
using Amazon.ResourceGroups.Model;
using Amazon.Runtime;

namespace CloudOps.ResourceGroups
{
    public class ListGroupsOperation : Operation
    {
        public override string Name => "ListGroups";

        public override string Description => "Returns a list of existing resource groups in your account.  Minimum permissions  To run this command, you must have the following permissions:    resource-groups:ListGroups   ";
 
        public override string RequestURI => "/groups-list";

        public override string Method => "POST";

        public override string ServiceName => "ResourceGroups";

        public override string ServiceID => "Resource Groups";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonResourceGroupsConfig config = new AmazonResourceGroupsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonResourceGroupsClient client = new AmazonResourceGroupsClient(creds, config);
            
            ListGroupsResponse resp = new ListGroupsResponse();
            do
            {
                try
                {
                    ListGroupsRequest req = new ListGroupsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListGroupsAsync(req);
                    
                    foreach (var obj in resp.GroupIdentifiers)
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