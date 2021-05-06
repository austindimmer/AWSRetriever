using Amazon;
using Amazon.Glue;
using Amazon.Glue.Model;
using Amazon.Runtime;

namespace CloudOps.Glue
{
    public class GetDevEndpointsOperation : Operation
    {
        public override string Name => "GetDevEndpoints";

        public override string Description => "Retrieves all the development endpoints in this AWS account.  When you create a development endpoint in a virtual private cloud (VPC), AWS Glue returns only a private IP address and the public IP address field is not populated. When you create a non-VPC development endpoint, AWS Glue returns only a public IP address. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Glue";

        public override string ServiceID => "Glue";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGlueConfig config = new AmazonGlueConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGlueClient client = new AmazonGlueClient(creds, config);
            
            GetDevEndpointsResponse resp = new GetDevEndpointsResponse();
            do
            {
                GetDevEndpointsRequest req = new GetDevEndpointsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.GetDevEndpointsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DevEndpoints)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}