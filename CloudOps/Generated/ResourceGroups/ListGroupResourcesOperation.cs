using Amazon;
using Amazon.ResourceGroups;
using Amazon.ResourceGroups.Model;
using Amazon.Runtime;

namespace CloudOps.ResourceGroups
{
    public class ListGroupResourcesOperation : Operation
    {
        public override string Name => "ListGroupResources";

        public override string Description => "Returns a list of ARNs of the resources that are members of a specified resource group.  Minimum permissions  To run this command, you must have the following permissions:    resource-groups:ListGroupResources   ";
 
        public override string RequestURI => "/list-group-resources";

        public override string Method => "POST";

        public override string ServiceName => "ResourceGroups";

        public override string ServiceID => "Resource Groups";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonResourceGroupsConfig config = new AmazonResourceGroupsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonResourceGroupsClient client = new AmazonResourceGroupsClient(creds, config);
            
            ListGroupResourcesResponse resp = new ListGroupResourcesResponse();
            do
            {
                ListGroupResourcesRequest req = new ListGroupResourcesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListGroupResources(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Resources)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.ResourceIdentifiers)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.QueryErrors)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}