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

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoboMakerConfig config = new AmazonRoboMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoboMakerClient client = new AmazonRoboMakerClient(creds, config);
            
            ListRobotApplicationsResponse resp = new ListRobotApplicationsResponse();
            do
            {
                ListRobotApplicationsRequest req = new ListRobotApplicationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListRobotApplications(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.RobotApplicationSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}