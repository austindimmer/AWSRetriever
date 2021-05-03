using Amazon;
using Amazon.Elasticsearch;
using Amazon.Elasticsearch.Model;
using Amazon.Runtime;

namespace CloudOps.Elasticsearch
{
    public class DescribePackagesOperation : Operation
    {
        public override string Name => "DescribePackages";

        public override string Description => "Describes all packages available to Amazon ES. Includes options for filtering, limiting the number of results, and pagination.";
 
        public override string RequestURI => "/2015-01-01/packages/describe";

        public override string Method => "POST";

        public override string ServiceName => "Elasticsearch";

        public override string ServiceID => "Elasticsearch Service";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticsearchConfig config = new AmazonElasticsearchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticsearchClient client = new AmazonElasticsearchClient(creds, config);
            
            DescribePackagesResponse resp = new DescribePackagesResponse();
            do
            {
                DescribePackagesRequest req = new DescribePackagesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribePackages(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PackageDetailsList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}