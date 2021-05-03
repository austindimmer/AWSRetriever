using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleSystemsManagement
{
    public class ListAssociationsOperation : Operation
    {
        public override string Name => "ListAssociations";

        public override string Description => "Returns all State Manager associations in the current AWS account and Region. You can limit the results to a specific State Manager association document or instance by specifying a filter.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SimpleSystemsManagement";

        public override string ServiceID => "SSM";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleSystemsManagementConfig config = new AmazonSimpleSystemsManagementConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleSystemsManagementClient client = new AmazonSimpleSystemsManagementClient(creds, config);
            
            ListAssociationsResponse resp = new ListAssociationsResponse();
            do
            {
                ListAssociationsRequest req = new ListAssociationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListAssociations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Associations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}