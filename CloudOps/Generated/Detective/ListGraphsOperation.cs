using Amazon;
using Amazon.Detective;
using Amazon.Detective.Model;
using Amazon.Runtime;

namespace CloudOps.Detective
{
    public class ListGraphsOperation : Operation
    {
        public override string Name => "ListGraphs";

        public override string Description => "Returns the list of behavior graphs that the calling account is an administrator account of. This operation can only be called by an administrator account. Because an account can currently only be the administrator of one behavior graph within a Region, the results always contain a single behavior graph.";
 
        public override string RequestURI => "/graphs/list";

        public override string Method => "POST";

        public override string ServiceName => "Detective";

        public override string ServiceID => "Detective";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDetectiveConfig config = new AmazonDetectiveConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDetectiveClient client = new AmazonDetectiveClient(creds, config);
            
            ListGraphsResponse resp = new ListGraphsResponse();
            do
            {
                try
                {
                    ListGraphsRequest req = new ListGraphsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListGraphsAsync(req);
                    
                    foreach (var obj in resp.GraphList)
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