using Amazon;
using Amazon.Translate;
using Amazon.Translate.Model;
using Amazon.Runtime;

namespace CloudOps.Translate
{
    public class ListParallelDataOperation : Operation
    {
        public override string Name => "ListParallelData";

        public override string Description => "Provides a list of your parallel data resources in Amazon Translate.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Translate";

        public override string ServiceID => "Translate";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonTranslateConfig config = new AmazonTranslateConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonTranslateClient client = new AmazonTranslateClient(creds, config);
            
            ListParallelDataResponse resp = new ListParallelDataResponse();
            do
            {
                try
                {
                    ListParallelDataRequest req = new ListParallelDataRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListParallelDataAsync(req);
                    
                    foreach (var obj in resp.ParallelDataPropertiesList)
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