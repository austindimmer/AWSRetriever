using Amazon;
using Amazon.LakeFormation;
using Amazon.LakeFormation.Model;
using Amazon.Runtime;

namespace CloudOps.LakeFormation
{
    public class ListResourcesOperation : Operation
    {
        public override string Name => "ListResources";

        public override string Description => "Lists the resources registered to be managed by the Data Catalog.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "LakeFormation";

        public override string ServiceID => "LakeFormation";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLakeFormationConfig config = new AmazonLakeFormationConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLakeFormationClient client = new AmazonLakeFormationClient(creds, config);
            
            ListResourcesResponse resp = new ListResourcesResponse();
            do
            {
                ListResourcesRequest req = new ListResourcesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListResourcesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ResourceInfoList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}