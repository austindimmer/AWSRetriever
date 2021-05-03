using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeCapacityReservationsOperation : Operation
    {
        public override string Name => "DescribeCapacityReservations";

        public override string Description => "Describes one or more of your Capacity Reservations. The results describe only the Capacity Reservations in the AWS Region that you&#39;re currently using.";
 
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
            
            DescribeCapacityReservationsResponse resp = new DescribeCapacityReservationsResponse();
            do
            {
                DescribeCapacityReservationsRequest req = new DescribeCapacityReservationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeCapacityReservations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CapacityReservations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}