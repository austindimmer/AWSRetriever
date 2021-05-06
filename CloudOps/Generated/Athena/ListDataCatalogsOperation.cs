using Amazon;
using Amazon.Athena;
using Amazon.Athena.Model;
using Amazon.Runtime;

namespace CloudOps.Athena
{
    public class ListDataCatalogsOperation : Operation
    {
        public override string Name => "ListDataCatalogs";

        public override string Description => "Lists the data catalogs in the current AWS account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Athena";

        public override string ServiceID => "Athena";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAthenaConfig config = new AmazonAthenaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAthenaClient client = new AmazonAthenaClient(creds, config);
            
            ListDataCatalogsResponse resp = new ListDataCatalogsResponse();
            do
            {
                try
                {
                    ListDataCatalogsRequest req = new ListDataCatalogsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDataCatalogsAsync(req);
                    
                    foreach (var obj in resp.DataCatalogsSummary)
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