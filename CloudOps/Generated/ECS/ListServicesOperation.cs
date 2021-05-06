using Amazon;
using Amazon.ECS;
using Amazon.ECS.Model;
using Amazon.Runtime;

namespace CloudOps.ECS
{
    public class ListServicesOperation : Operation
    {
        public override string Name => "ListServices";

        public override string Description => "Lists the services that are running in a specified cluster.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ECS";

        public override string ServiceID => "ECS";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonECSConfig config = new AmazonECSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonECSClient client = new AmazonECSClient(creds, config);
            
            ListServicesResponse resp = new ListServicesResponse();
            do
            {
                try
                {
                    ListServicesRequest req = new ListServicesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListServicesAsync(req);
                    
                    foreach (var obj in resp.ServiceArns)
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