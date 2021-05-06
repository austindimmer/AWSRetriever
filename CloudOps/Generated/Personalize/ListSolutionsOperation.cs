using Amazon;
using Amazon.Personalize;
using Amazon.Personalize.Model;
using Amazon.Runtime;

namespace CloudOps.Personalize
{
    public class ListSolutionsOperation : Operation
    {
        public override string Name => "ListSolutions";

        public override string Description => "Returns a list of solutions that use the given dataset group. When a dataset group is not specified, all the solutions associated with the account are listed. The response provides the properties for each solution, including the Amazon Resource Name (ARN). For more information on solutions, see CreateSolution.";
 
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
            
            ListSolutionsResponse resp = new ListSolutionsResponse();
            do
            {
                try
                {
                    ListSolutionsRequest req = new ListSolutionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListSolutionsAsync(req);
                    
                    foreach (var obj in resp.Solutions)
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