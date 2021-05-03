using Amazon;
using Amazon.Athena;
using Amazon.Athena.Model;
using Amazon.Runtime;

namespace CloudOps.Athena
{
    public class ListPreparedStatementsOperation : Operation
    {
        public override string Name => "ListPreparedStatements";

        public override string Description => "Lists the prepared statements in the specfied workgroup.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Athena";

        public override string ServiceID => "Athena";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAthenaConfig config = new AmazonAthenaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAthenaClient client = new AmazonAthenaClient(creds, config);
            
            ListPreparedStatementsResponse resp = new ListPreparedStatementsResponse();
            do
            {
                ListPreparedStatementsRequest req = new ListPreparedStatementsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListPreparedStatements(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PreparedStatements)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}