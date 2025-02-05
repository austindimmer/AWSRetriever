using Amazon;
using Amazon.Neptune;
using Amazon.Neptune.Model;
using Amazon.Runtime;

namespace CloudOps.Neptune
{
    public class DescribeDBParameterGroupsOperation : Operation
    {
        public override string Name => "DescribeDBParameterGroups";

        public override string Description => "Returns a list of DBParameterGroup descriptions. If a DBParameterGroupName is specified, the list will contain only the description of the specified DB parameter group.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Neptune";

        public override string ServiceID => "Neptune";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonNeptuneConfig config = new AmazonNeptuneConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonNeptuneClient client = new AmazonNeptuneClient(creds, config);
            
            DescribeDBParameterGroupsResponse resp = new DescribeDBParameterGroupsResponse();
            do
            {
                DescribeDBParameterGroupsRequest req = new DescribeDBParameterGroupsRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeDBParameterGroups(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DBParameterGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}