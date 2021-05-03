using Amazon;
using Amazon.LocationService;
using Amazon.LocationService.Model;
using Amazon.Runtime;

namespace CloudOps.LocationService
{
    public class ListPlaceIndexesOperation : Operation
    {
        public override string Name => "ListPlaceIndexes";

        public override string Description => "Lists Place index resources in your AWS account.";
 
        public override string RequestURI => "/places/v0/list-indexes";

        public override string Method => "POST";

        public override string ServiceName => "LocationService";

        public override string ServiceID => "Location";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLocationServiceConfig config = new AmazonLocationServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLocationServiceClient client = new AmazonLocationServiceClient(creds, config);
            
            ListPlaceIndexesResponse resp = new ListPlaceIndexesResponse();
            do
            {
                ListPlaceIndexesRequest req = new ListPlaceIndexesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListPlaceIndexes(req);
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