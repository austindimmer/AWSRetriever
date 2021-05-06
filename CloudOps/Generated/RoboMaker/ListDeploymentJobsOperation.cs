using Amazon;
using Amazon.RoboMaker;
using Amazon.RoboMaker.Model;
using Amazon.Runtime;

namespace CloudOps.RoboMaker
{
    public class ListDeploymentJobsOperation : Operation
    {
        public override string Name => "ListDeploymentJobs";

        public override string Description => "Returns a list of deployment jobs for a fleet. You can optionally provide filters to retrieve specific deployment jobs. ";
 
        public override string RequestURI => "/listDeploymentJobs";

        public override string Method => "POST";

        public override string ServiceName => "RoboMaker";

        public override string ServiceID => "RoboMaker";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoboMakerConfig config = new AmazonRoboMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoboMakerClient client = new AmazonRoboMakerClient(creds, config);
            
            ListDeploymentJobsResponse resp = new ListDeploymentJobsResponse();
            do
            {
                try
                {
                    ListDeploymentJobsRequest req = new ListDeploymentJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDeploymentJobsAsync(req);
                    
                    foreach (var obj in resp.DeploymentJobs)
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