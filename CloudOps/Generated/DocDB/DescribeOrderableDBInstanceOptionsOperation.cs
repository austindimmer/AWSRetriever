using Amazon;
using Amazon.DocDB;
using Amazon.DocDB.Model;
using Amazon.Runtime;

namespace CloudOps.DocDB
{
    public class DescribeOrderableDBInstanceOptionsOperation : Operation
    {
        public override string Name => "DescribeOrderableDBInstanceOptions";

        public override string Description => "Returns a list of orderable instance options for the specified engine.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DocDB";

        public override string ServiceID => "DocDB";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDocDBConfig config = new AmazonDocDBConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDocDBClient client = new AmazonDocDBClient(creds, config);
            
            DescribeOrderableDBInstanceOptionsResponse resp = new DescribeOrderableDBInstanceOptionsResponse();
            do
            {
                try
                {
                    DescribeOrderableDBInstanceOptionsRequest req = new DescribeOrderableDBInstanceOptionsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeOrderableDBInstanceOptionsAsync(req);
                    
                    foreach (var obj in resp.OrderableDBInstanceOptions)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}