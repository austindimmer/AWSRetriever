using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class SearchTransitGatewayMulticastGroupsOperation : Operation
    {
        public override string Name => "SearchTransitGatewayMulticastGroups";

        public override string Description => "Searches one or more transit gateway multicast groups and returns the group membership information.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            SearchTransitGatewayMulticastGroupsResponse resp = new SearchTransitGatewayMulticastGroupsResponse();
            do
            {
                SearchTransitGatewayMulticastGroupsRequest req = new SearchTransitGatewayMulticastGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.SearchTransitGatewayMulticastGroupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.MulticastGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}