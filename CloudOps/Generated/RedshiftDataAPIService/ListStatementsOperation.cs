using Amazon;
using Amazon.RedshiftDataAPIService;
using Amazon.RedshiftDataAPIService.Model;
using Amazon.Runtime;

namespace CloudOps.RedshiftDataAPIService
{
    public class ListStatementsOperation : Operation
    {
        public override string Name => "ListStatements";

        public override string Description => "List of SQL statements. By default, only finished statements are shown. A token is returned to page through the statement list. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "RedshiftDataAPIService";

        public override string ServiceID => "Redshift Data";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRedshiftDataAPIServiceConfig config = new AmazonRedshiftDataAPIServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRedshiftDataAPIServiceClient client = new AmazonRedshiftDataAPIServiceClient(creds, config);
            
            ListStatementsResponse resp = new ListStatementsResponse();
            do
            {
                try
                {
                    ListStatementsRequest req = new ListStatementsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListStatementsAsync(req);
                    
                    foreach (var obj in resp.Statements)
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