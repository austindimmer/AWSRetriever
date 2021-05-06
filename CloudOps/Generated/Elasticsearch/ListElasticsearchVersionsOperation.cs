using Amazon;
using Amazon.Elasticsearch;
using Amazon.Elasticsearch.Model;
using Amazon.Runtime;

namespace CloudOps.Elasticsearch
{
    public class ListElasticsearchVersionsOperation : Operation
    {
        public override string Name => "ListElasticsearchVersions";

        public override string Description => "List all supported Elasticsearch versions";
 
        public override string RequestURI => "/2015-01-01/es/versions";

        public override string Method => "GET";

        public override string ServiceName => "Elasticsearch";

        public override string ServiceID => "Elasticsearch Service";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticsearchConfig config = new AmazonElasticsearchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticsearchClient client = new AmazonElasticsearchClient(creds, config);
            
            ListElasticsearchVersionsResponse resp = new ListElasticsearchVersionsResponse();
            do
            {
                try
                {
                    ListElasticsearchVersionsRequest req = new ListElasticsearchVersionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListElasticsearchVersionsAsync(req);
                    
                    foreach (var obj in resp.ElasticsearchVersions)
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