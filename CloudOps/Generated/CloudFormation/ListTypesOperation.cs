using Amazon;
using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.Runtime;

namespace CloudOps.CloudFormation
{
    public class ListTypesOperation : Operation
    {
        public override string Name => "ListTypes";

        public override string Description => "Returns summary information about extension that have been registered with CloudFormation.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudFormation";

        public override string ServiceID => "CloudFormation";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudFormationConfig config = new AmazonCloudFormationConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudFormationClient client = new AmazonCloudFormationClient(creds, config);
            
            ListTypesResponse resp = new ListTypesResponse();
            do
            {
                ListTypesRequest req = new ListTypesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListTypes(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TypeSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}