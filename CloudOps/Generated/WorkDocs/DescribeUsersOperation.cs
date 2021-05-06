using Amazon;
using Amazon.WorkDocs;
using Amazon.WorkDocs.Model;
using Amazon.Runtime;

namespace CloudOps.WorkDocs
{
    public class DescribeUsersOperation : Operation
    {
        public override string Name => "DescribeUsers";

        public override string Description => "Describes the specified users. You can describe all users or filter the results (for example, by status or organization). By default, Amazon WorkDocs returns the first 24 active or pending users. If there are more results, the response includes a marker that you can use to request the next set of results.";
 
        public override string RequestURI => "/api/v1/users";

        public override string Method => "GET";

        public override string ServiceName => "WorkDocs";

        public override string ServiceID => "WorkDocs";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWorkDocsConfig config = new AmazonWorkDocsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWorkDocsClient client = new AmazonWorkDocsClient(creds, config);
            
            DescribeUsersResponse resp = new DescribeUsersResponse();
            do
            {
                DescribeUsersRequest req = new DescribeUsersRequest
                {
                    Marker = resp.Marker
                    ,
                    Limit = maxItems
                                        
                };

                resp = await client.DescribeUsersAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Users)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}