using Amazon;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Amazon.Runtime;

namespace CloudOps.Comprehend
{
    public class ListEventsDetectionJobsOperation : Operation
    {
        public override string Name => "ListEventsDetectionJobs";

        public override string Description => "Gets a list of the events detection jobs that you have submitted.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Comprehend";

        public override string ServiceID => "Comprehend";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonComprehendConfig config = new AmazonComprehendConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonComprehendClient client = new AmazonComprehendClient(creds, config);
            
            ListEventsDetectionJobsResponse resp = new ListEventsDetectionJobsResponse();
            do
            {
                ListEventsDetectionJobsRequest req = new ListEventsDetectionJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListEventsDetectionJobs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.EventsDetectionJobPropertiesList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}