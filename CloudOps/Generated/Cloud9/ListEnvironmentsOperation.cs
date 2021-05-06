using Amazon;
using Amazon.Cloud9;
using Amazon.Cloud9.Model;
using Amazon.Runtime;

namespace CloudOps.Cloud9
{
    public class ListEnvironmentsOperation : Operation
    {
        public override string Name => "ListEnvironments";

        public override string Description => "Gets a list of AWS Cloud9 development environment identifiers.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Cloud9";

        public override string ServiceID => "Cloud9";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloud9Config config = new AmazonCloud9Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloud9Client client = new AmazonCloud9Client(creds, config);
            
            ListEnvironmentsResponse resp = new ListEnvironmentsResponse();
            do
            {
                try
                {
                    ListEnvironmentsRequest req = new ListEnvironmentsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListEnvironmentsAsync(req);
                    
                    foreach (var obj in resp.EnvironmentIds)
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