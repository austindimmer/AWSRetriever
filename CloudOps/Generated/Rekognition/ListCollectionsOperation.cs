using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.Runtime;

namespace CloudOps.Rekognition
{
    public class ListCollectionsOperation : Operation
    {
        public override string Name => "ListCollections";

        public override string Description => "Returns list of collection IDs in your account. If the result is truncated, the response also provides a NextToken that you can use in the subsequent request to fetch the next set of collection IDs. For an example, see Listing Collections in the Amazon Rekognition Developer Guide. This operation requires permissions to perform the rekognition:ListCollections action.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Rekognition";

        public override string ServiceID => "Rekognition";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRekognitionConfig config = new AmazonRekognitionConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRekognitionClient client = new AmazonRekognitionClient(creds, config);
            
            ListCollectionsResponse resp = new ListCollectionsResponse();
            do
            {
                try
                {
                    ListCollectionsRequest req = new ListCollectionsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListCollectionsAsync(req);
                    
                    foreach (var obj in resp.CollectionIds)
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