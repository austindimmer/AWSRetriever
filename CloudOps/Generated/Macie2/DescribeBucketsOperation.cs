using Amazon;
using Amazon.Macie;
using Amazon.Macie.Model;
using Amazon.Runtime;

namespace CloudOps.Macie
{
    public class DescribeBucketsOperation : Operation
    {
        public override string Name => "DescribeBuckets";

        public override string Description => " Retrieves (queries) statistical data and other information about one or more S3 buckets that Amazon Macie monitors and analyzes.";
 
        public override string RequestURI => "/datasources/s3";

        public override string Method => "POST";

        public override string ServiceName => "Macie";

        public override string ServiceID => "Macie";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacieConfig config = new AmazonMacieConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacieClient client = new AmazonMacieClient(creds, config);
            
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