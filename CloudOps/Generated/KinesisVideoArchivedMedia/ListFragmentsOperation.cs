using Amazon;
using Amazon.KinesisVideoArchivedMedia;
using Amazon.KinesisVideoArchivedMedia.Model;
using Amazon.Runtime;

namespace CloudOps.KinesisVideoArchivedMedia
{
    public class ListFragmentsOperation : Operation
    {
        public override string Name => "ListFragments";

        public override string Description => "Returns a list of Fragment objects from the specified stream and timestamp range within the archived data. Listing fragments is eventually consistent. This means that even if the producer receives an acknowledgment that a fragment is persisted, the result might not be returned immediately from a request to ListFragments. However, results are typically available in less than one second.  You must first call the GetDataEndpoint API to get an endpoint. Then send the ListFragments requests to this endpoint using the --endpoint-url parameter.    If an error is thrown after invoking a Kinesis Video Streams archived media API, in addition to the HTTP status code and the response body, it includes the following pieces of information:     x-amz-ErrorType HTTP header – contains a more specific error type in addition to what the HTTP status code provides.     x-amz-RequestId HTTP header – if you want to report an issue to AWS, the support team can better diagnose the problem if given the Request Id.   Both the HTTP status code and the ErrorType header can be utilized to make programmatic decisions about whether errors are retry-able and under what conditions, as well as provide information on what actions the client programmer might need to take in order to successfully try again. For more information, see the Errors section at the bottom of this topic, as well as Common Errors.  ";
 
        public override string RequestURI => "/listFragments";

        public override string Method => "POST";

        public override string ServiceName => "KinesisVideoArchivedMedia";

        public override string ServiceID => "Kinesis Video Archived Media";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonKinesisVideoArchivedMediaConfig config = new AmazonKinesisVideoArchivedMediaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonKinesisVideoArchivedMediaClient client = new AmazonKinesisVideoArchivedMediaClient(creds, config);
            
            ListFragmentsResponse resp = new ListFragmentsResponse();
            do
            {
                try
                {
                    ListFragmentsRequest req = new ListFragmentsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListFragmentsAsync(req);
                    
                    foreach (var obj in resp.Fragments)
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