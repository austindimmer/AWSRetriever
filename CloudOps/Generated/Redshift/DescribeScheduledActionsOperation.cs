using Amazon;
using Amazon.Redshift;
using Amazon.Redshift.Model;
using Amazon.Runtime;

namespace CloudOps.Redshift
{
    public class DescribeScheduledActionsOperation : Operation
    {
        public override string Name => "DescribeScheduledActions";

        public override string Description => "Describes properties of scheduled actions. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Redshift";

        public override string ServiceID => "Redshift";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRedshiftConfig config = new AmazonRedshiftConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRedshiftClient client = new AmazonRedshiftClient(creds, config);
            
            DescribeScheduledActionsResponse resp = new DescribeScheduledActionsResponse();
            do
            {
                DescribeScheduledActionsRequest req = new DescribeScheduledActionsRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeScheduledActions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ScheduledActions)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}