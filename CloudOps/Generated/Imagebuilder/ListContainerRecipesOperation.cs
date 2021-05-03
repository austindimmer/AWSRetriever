using Amazon;
using Amazon.Imagebuilder;
using Amazon.Imagebuilder.Model;
using Amazon.Runtime;

namespace CloudOps.Imagebuilder
{
    public class ListContainerRecipesOperation : Operation
    {
        public override string Name => "ListContainerRecipes";

        public override string Description => "Returns a list of container recipes.";
 
        public override string RequestURI => "/ListContainerRecipes";

        public override string Method => "POST";

        public override string ServiceName => "Imagebuilder";

        public override string ServiceID => "imagebuilder";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonImagebuilderConfig config = new AmazonImagebuilderConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonImagebuilderClient client = new AmazonImagebuilderClient(creds, config);
            
            ListContainerRecipesResponse resp = new ListContainerRecipesResponse();
            do
            {
                ListContainerRecipesRequest req = new ListContainerRecipesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListContainerRecipes(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ContainerRecipeSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}