using Amazon;
using Amazon.DocDB;
using Amazon.DocDB.Model;
using Amazon.Runtime;

namespace CloudOps.DocDB
{
    public class DescribeDBClusterParameterGroupsOperation : Operation
    {
        public override string Name => "DescribeDBClusterParameterGroups";

        public override string Description => "Returns a list of DBClusterParameterGroup descriptions. If a DBClusterParameterGroupName parameter is specified, the list contains only the description of the specified cluster parameter group. ";
 
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
            
            DescribeDBClusterParameterGroupsResponse resp = new DescribeDBClusterParameterGroupsResponse();
            do
            {
                try
                {
                    DescribeDBClusterParameterGroupsRequest req = new DescribeDBClusterParameterGroupsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeDBClusterParameterGroupsAsync(req);
                    
                    foreach (var obj in resp.DBClusterParameterGroups)
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