using Amazon;
using Amazon.GlueDataBrew;
using Amazon.GlueDataBrew.Model;
using Amazon.Runtime;

namespace CloudOps.GlueDataBrew
{
    public class ListRecipesOperation : Operation
    {
        public override string Name => "ListRecipes";

        public override string Description => "Lists all of the DataBrew recipes that are defined.";
 
        public override string RequestURI => "/recipes";

        public override string Method => "GET";

        public override string ServiceName => "GlueDataBrew";

        public override string ServiceID => "DataBrew";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGlueDataBrewConfig config = new AmazonGlueDataBrewConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGlueDataBrewClient client = new AmazonGlueDataBrewClient(creds, config);
            
            ListRecipesResponse resp = new ListRecipesResponse();
            do
            {
                ListRecipesRequest req = new ListRecipesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListRecipesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Recipes)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}