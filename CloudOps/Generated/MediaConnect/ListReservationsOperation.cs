using Amazon;
using Amazon.MediaConnect;
using Amazon.MediaConnect.Model;
using Amazon.Runtime;

namespace CloudOps.MediaConnect
{
    public class ListReservationsOperation : Operation
    {
        public override string Name => "ListReservations";

        public override string Description => "Displays a list of all reservations that have been purchased by this account in the current AWS Region. This list includes all reservations in all states (such as active and expired).";
 
        public override string RequestURI => "/v1/reservations";

        public override string Method => "GET";

        public override string ServiceName => "MediaConnect";

        public override string ServiceID => "MediaConnect";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaConnectConfig config = new AmazonMediaConnectConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaConnectClient client = new AmazonMediaConnectClient(creds, config);
            
            ListReservationsResponse resp = new ListReservationsResponse();
            do
            {
                ListReservationsRequest req = new ListReservationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListReservationsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Reservations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}