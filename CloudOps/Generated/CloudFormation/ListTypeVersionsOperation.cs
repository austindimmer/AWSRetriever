using Amazon;
using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.Runtime;

namespace CloudOps.CloudFormation
{
    public class ListTypeVersionsOperation : Operation
    {
        public override string Name => "ListTypeVersions";

        public override string Description => "Returns summary information about the versions of an extension.";
 
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
            
            ListTypeVersionsResponse resp = new ListTypeVersionsResponse();
            do
            {
                try
                {
                    ListTypeVersionsRequest req = new ListTypeVersionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListTypeVersionsAsync(req);
                    
                    foreach (var obj in resp.TypeVersionSummaries)
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