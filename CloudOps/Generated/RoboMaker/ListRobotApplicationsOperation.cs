using Amazon;
using Amazon.RoboMaker;
using Amazon.RoboMaker.Model;
using Amazon.Runtime;

namespace CloudOps.RoboMaker
{
    public class ListRobotApplicationsOperation : Operation
    {
        public override string Name => "ListRobotApplications";

        public override string Description => "Returns a list of robot application. You can optionally provide filters to retrieve specific robot applications.";
 
        public override string RequestURI => "/listRobotApplications";

        public override string Method => "POST";

        public override string ServiceName => "RoboMaker";

        public override string ServiceID => "RoboMaker";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoboMakerConfig config = new AmazonRoboMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoboMakerClient client = new AmazonRoboMakerClient(creds, config);
            
            ListRobotApplicationsResponse resp = new ListRobotApplicationsResponse();
            do
            {
                try
                {
                    ListRobotApplicationsRequest req = new ListRobotApplicationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListRobotApplicationsAsync(req);
                    
                    foreach (var obj in resp.RobotApplicationSummaries)
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