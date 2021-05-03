using Amazon;
using Amazon.Imagebuilder;
using Amazon.Imagebuilder.Model;
using Amazon.Runtime;

namespace CloudOps.Imagebuilder
{
    public class ListImageRecipesOperation : Operation
    {
        public override string Name => "ListImageRecipes";

        public override string Description => " Returns a list of image recipes.";
 
        public override string RequestURI => "/ListImageRecipes";

        public override string Method => "POST";

        public override string ServiceName => "Imagebuilder";

        public override string ServiceID => "imagebuilder";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonImagebuilderConfig config = new AmazonImagebuilderConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonImagebuilderClient client = new AmazonImagebuilderClient(creds, config);
            
            ListImageRecipesResponse resp = new ListImageRecipesResponse();
            do
            {
                ListImageRecipesRequest req = new ListImageRecipesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListImageRecipes(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ImageRecipeSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}