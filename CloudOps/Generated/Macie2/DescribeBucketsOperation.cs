using Amazon;
using Amazon.Macie2;
using Amazon.Macie2.Model;
using Amazon.Runtime;

namespace CloudOps.Macie2
{
    public class DescribeBucketsOperation : Operation
    {
        public override string Name => "DescribeBuckets";

        public override string Description => " Retrieves (queries) statistical data and other information about one or more S3 buckets that Amazon Macie monitors and analyzes.";
 
        public override string RequestURI => "/datasources/s3";

        public override string Method => "POST";

        public override string ServiceName => "Macie2";

        public override string ServiceID => "Macie2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacie2Config config = new AmazonMacie2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacie2Client client = new AmazonMacie2Client(creds, config);
            
            DescribeBucketsResponse resp = new DescribeBucketsResponse();
            do
            {
                DescribeBucketsRequest req = new DescribeBucketsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeBuckets(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Buckets)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}