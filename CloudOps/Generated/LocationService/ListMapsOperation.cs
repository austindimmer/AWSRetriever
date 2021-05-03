using Amazon;
using Amazon.LocationService;
using Amazon.LocationService.Model;
using Amazon.Runtime;

namespace CloudOps.LocationService
{
    public class ListMapsOperation : Operation
    {
        public override string Name => "ListMaps";

        public override string Description => "Lists map resources in your AWS account.";
 
        public override string RequestURI => "/maps/v0/list-maps";

        public override string Method => "POST";

        public override string ServiceName => "LocationService";

        public override string ServiceID => "Location";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLocationServiceConfig config = new AmazonLocationServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLocationServiceClient client = new AmazonLocationServiceClient(creds, config);
            
            ListMapsResponse resp = new ListMapsResponse();
            do
            {
                ListMapsRequest req = new ListMapsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListMaps(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Entries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}