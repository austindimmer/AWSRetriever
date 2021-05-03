using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class GetTransitGatewayMulticastDomainAssociationsOperation : Operation
    {
        public override string Name => "GetTransitGatewayMulticastDomainAssociations";

        public override string Description => "Gets information about the associations for the transit gateway multicast domain.";
 
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
            
            GetTransitGatewayMulticastDomainAssociationsResponse resp = new GetTransitGatewayMulticastDomainAssociationsResponse();
            do
            {
                GetTransitGatewayMulticastDomainAssociationsRequest req = new GetTransitGatewayMulticastDomainAssociationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.GetTransitGatewayMulticastDomainAssociations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.MulticastDomainAssociations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}