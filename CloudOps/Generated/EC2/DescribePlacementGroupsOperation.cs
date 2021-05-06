using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribePlacementGroupsOperation : Operation
    {
        public override string Name => "DescribePlacementGroups";

        public override string Description => "Describes the specified placement groups or all of your placement groups. For more information, see Placement groups in the Amazon EC2 User Guide.";
 
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
            
            DescribePlacementGroupsResponse resp = new DescribePlacementGroupsResponse();
            DescribePlacementGroupsRequest req = new DescribePlacementGroupsRequest
            {                    
                                    
            };
            
            try
            {
                resp = await client.DescribePlacementGroupsAsync(req);
                
                foreach (var obj in resp.PlacementGroups)
                {
                    AddObject(obj);
                }
                
            }
            catch (System.Exception)
            {
                CheckError(resp.HttpStatusCode, "200");                
                throw;
            }
            
        }
    }
}