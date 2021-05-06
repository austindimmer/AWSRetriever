using Amazon;
using Amazon.Imagebuilder;
using Amazon.Imagebuilder.Model;
using Amazon.Runtime;

namespace CloudOps.Imagebuilder
{
    public class ListImagesOperation : Operation
    {
        public override string Name => "ListImages";

        public override string Description => " Returns the list of images that you have access to.";
 
        public override string RequestURI => "/ListImages";

        public override string Method => "POST";

        public override string ServiceName => "Imagebuilder";

        public override string ServiceID => "imagebuilder";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonImagebuilderConfig config = new AmazonImagebuilderConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonImagebuilderClient client = new AmazonImagebuilderClient(creds, config);
            
            ListImagesResponse resp = new ListImagesResponse();
            do
            {
                try
                {
                    ListImagesRequest req = new ListImagesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListImagesAsync(req);
                    
                    foreach (var obj in resp.ImageVersionList)
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