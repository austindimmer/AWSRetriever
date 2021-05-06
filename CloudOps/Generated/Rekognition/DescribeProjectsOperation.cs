using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.Runtime;

namespace CloudOps.Rekognition
{
    public class DescribeProjectsOperation : Operation
    {
        public override string Name => "DescribeProjects";

        public override string Description => "Lists and gets information about your Amazon Rekognition Custom Labels projects. This operation requires permissions to perform the rekognition:DescribeProjects action.";
 
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
            
            DescribeProjectsResponse resp = new DescribeProjectsResponse();
            do
            {
                try
                {
                    DescribeProjectsRequest req = new DescribeProjectsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeProjectsAsync(req);
                    
                    foreach (var obj in resp.ProjectDescriptions)
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