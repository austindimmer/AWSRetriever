using Amazon;
using Amazon.TimestreamWrite;
using Amazon.TimestreamWrite.Model;
using Amazon.Runtime;

namespace CloudOps.TimestreamWrite
{
    public class ListTablesOperation : Operation
    {
        public override string Name => "ListTables";

        public override string Description => "A list of tables, along with the name, status and retention properties of each table. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "TimestreamWrite";

        public override string ServiceID => "Timestream Write";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonTimestreamWriteConfig config = new AmazonTimestreamWriteConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonTimestreamWriteClient client = new AmazonTimestreamWriteClient(creds, config);
            
            ListTablesResponse resp = new ListTablesResponse();
            do
            {
                ListTablesRequest req = new ListTablesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListTables(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Tables)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}