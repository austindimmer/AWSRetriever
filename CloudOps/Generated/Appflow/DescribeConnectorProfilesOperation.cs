using Amazon;
using Amazon.Appflow;
using Amazon.Appflow.Model;
using Amazon.Runtime;

namespace CloudOps.Appflow
{
    public class DescribeConnectorProfilesOperation : Operation
    {
        public override string Name => "DescribeConnectorProfiles";

        public override string Description => " Returns a list of connector-profile details matching the provided connector-profile names and connector-types. Both input lists are optional, and you can use them to filter the result.  If no names or connector-types are provided, returns all connector profiles in a paginated form. If there is no match, this operation returns an empty list.";
 
        public override string RequestURI => "/describe-connector-profiles";

        public override string Method => "POST";

        public override string ServiceName => "Appflow";

        public override string ServiceID => "Appflow";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppflowConfig config = new AmazonAppflowConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppflowClient client = new AmazonAppflowClient(creds, config);
            
            DescribeConnectorProfilesResponse resp = new DescribeConnectorProfilesResponse();
            do
            {
                DescribeConnectorProfilesRequest req = new DescribeConnectorProfilesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeConnectorProfilesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ConnectorProfileDetails)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}