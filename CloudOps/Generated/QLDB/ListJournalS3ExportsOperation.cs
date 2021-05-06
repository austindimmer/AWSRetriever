using Amazon;
using Amazon.QLDB;
using Amazon.QLDB.Model;
using Amazon.Runtime;

namespace CloudOps.QLDB
{
    public class ListJournalS3ExportsOperation : Operation
    {
        public override string Name => "ListJournalS3Exports";

        public override string Description => "Returns an array of journal export job descriptions for all ledgers that are associated with the current AWS account and Region. This action returns a maximum of MaxResults items, and is paginated so that you can retrieve all the items by calling ListJournalS3Exports multiple times. This action does not return any expired export jobs. For more information, see Export Job Expiration in the Amazon QLDB Developer Guide.";
 
        public override string RequestURI => "/journal-s3-exports";

        public override string Method => "GET";

        public override string ServiceName => "QLDB";

        public override string ServiceID => "QLDB";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonQLDBConfig config = new AmazonQLDBConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonQLDBClient client = new AmazonQLDBClient(creds, config);
            
            ListJournalS3ExportsResponse resp = new ListJournalS3ExportsResponse();
            do
            {
                try
                {
                    ListJournalS3ExportsRequest req = new ListJournalS3ExportsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListJournalS3ExportsAsync(req);
                    
                    foreach (var obj in resp.JournalS3Exports)
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