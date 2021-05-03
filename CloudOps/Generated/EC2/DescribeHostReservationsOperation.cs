using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeHostReservationsOperation : Operation
    {
        public override string Name => "DescribeHostReservations";

        public override string Description => "Describes reservations that are associated with Dedicated Hosts in your account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            DescribeHostReservationsResponse resp = new DescribeHostReservationsResponse();
            do
            {
                DescribeHostReservationsRequest req = new DescribeHostReservationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeHostReservations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.HostReservationSet)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}