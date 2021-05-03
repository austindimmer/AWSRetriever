using Amazon;
using Amazon.S3Outposts;
using Amazon.S3Outposts.Model;
using Amazon.Runtime;

namespace CloudOps.S3Outposts
{
    public class ListEndpointsOperation : Operation
    {
        public override string Name => "ListEndpoints";

        public override string Description => "S3 on Outposts access points simplify managing data access at scale for shared datasets in Amazon S3 on Outposts. S3 on Outposts uses endpoints to connect to Outposts buckets so that you can perform actions within your virtual private cloud (VPC).  This action lists endpoints associated with the Outpost.   Related actions include:    CreateEndpoint     DeleteEndpoint   ";
 
        public override string RequestURI => "/S3Outposts/ListEndpoints";

        public override string Method => "GET";

        public override string ServiceName => "S3Outposts";

        public override string ServiceID => "S3Outposts";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonS3OutpostsConfig config = new AmazonS3OutpostsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonS3OutpostsClient client = new AmazonS3OutpostsClient(creds, config);
            
            ListEndpointsResponse resp = new ListEndpointsResponse();
            do
            {
                ListEndpointsRequest req = new ListEndpointsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListEndpoints(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Endpoints)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}