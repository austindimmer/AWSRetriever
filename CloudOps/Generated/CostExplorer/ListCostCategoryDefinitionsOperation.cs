using Amazon;
using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;
using Amazon.Runtime;

namespace CloudOps.CostExplorer
{
    public class ListCostCategoryDefinitionsOperation : Operation
    {
        public override string Name => "ListCostCategoryDefinitions";

        public override string Description => "Returns the name, ARN, NumberOfRules and effective dates of all Cost Categories defined in the account. You have the option to use EffectiveOn to return a list of Cost Categories that were active on a specific date. If there is no EffectiveOn specified, you’ll see Cost Categories that are effective on the current date. If Cost Category is still effective, EffectiveEnd is omitted in the response. ListCostCategoryDefinitions supports pagination. The request can have a MaxResults range up to 100.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CostExplorer";

        public override string ServiceID => "Cost Explorer";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCostExplorerConfig config = new AmazonCostExplorerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCostExplorerClient client = new AmazonCostExplorerClient(creds, config);
            
            ListCostCategoryDefinitionsResponse resp = new ListCostCategoryDefinitionsResponse();
            do
            {
                ListCostCategoryDefinitionsRequest req = new ListCostCategoryDefinitionsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListCostCategoryDefinitions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CostCategoryReferences)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}