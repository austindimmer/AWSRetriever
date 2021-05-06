using Amazon;
using Amazon.Athena;
using Amazon.Athena.Model;
using Amazon.Runtime;

namespace CloudOps.Athena
{
    public class GetQueryResultsOperation : Operation
    {
        public override string Name => "GetQueryResults";

        public override string Description => "Streams the results of a single query execution specified by QueryExecutionId from the Athena query results location in Amazon S3. For more information, see Query Results in the Amazon Athena User Guide. This request does not execute the query but returns results. Use StartQueryExecution to run a query. To stream query results successfully, the IAM principal with permission to call GetQueryResults also must have permissions to the Amazon S3 GetObject action for the Athena query results location.  IAM principals with permission to the Amazon S3 GetObject action for the query results location are able to retrieve query results from Amazon S3 even if permission to the GetQueryResults action is denied. To restrict user or role access, ensure that Amazon S3 permissions to the Athena query location are denied. ";
 
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
            
            GetQueryResultsResponse resp = new GetQueryResultsResponse();
            do
            {
                GetQueryResultsRequest req = new GetQueryResultsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.GetQueryResults(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.UpdateCount)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.ResultSet)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}