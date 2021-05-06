using Amazon;
using Amazon.Personalize;
using Amazon.Personalize.Model;
using Amazon.Runtime;

namespace CloudOps.Personalize
{
    public class ListRecipesOperation : Operation
    {
        public override string Name => "ListRecipes";

        public override string Description => "Returns a list of available recipes. The response provides the properties for each recipe, including the recipe&#39;s Amazon Resource Name (ARN).";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Personalize";

        public override string ServiceID => "Personalize";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPersonalizeConfig config = new AmazonPersonalizeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPersonalizeClient client = new AmazonPersonalizeClient(creds, config);
            
            ListRecipesResponse resp = new ListRecipesResponse();
            do
            {
                try
                {
                    ListRecipesRequest req = new ListRecipesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListRecipesAsync(req);
                    
                    foreach (var obj in resp.Recipes)
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
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}