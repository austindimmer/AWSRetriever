using Amazon;
using Amazon.CloudFormation;
using Amazon.CloudFormation.Model;
using Amazon.Runtime;

namespace CloudOps.CloudFormation
{
    public class ListTypeRegistrationsOperation : Operation
    {
        public override string Name => "ListTypeRegistrations";

        public override string Description => "Returns a list of registration tokens for the specified extension(s).";
 
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
            
            ListTypeRegistrationsResponse resp = new ListTypeRegistrationsResponse();
            do
            {
                ListTypeRegistrationsRequest req = new ListTypeRegistrationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListTypeRegistrations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.RegistrationTokenList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}