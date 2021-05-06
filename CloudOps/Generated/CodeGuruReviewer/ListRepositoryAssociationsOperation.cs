using Amazon;
using Amazon.CodeGuruReviewer;
using Amazon.CodeGuruReviewer.Model;
using Amazon.Runtime;

namespace CloudOps.CodeGuruReviewer
{
    public class ListRepositoryAssociationsOperation : Operation
    {
        public override string Name => "ListRepositoryAssociations";

        public override string Description => " Returns a list of  RepositoryAssociationSummary  objects that contain summary information about a repository association. You can filter the returned list by  ProviderType ,  Name ,  State , and  Owner . ";
 
        public override string RequestURI => "/associations";

        public override string Method => "GET";

        public override string ServiceName => "CodeGuruReviewer";

        public override string ServiceID => "CodeGuru Reviewer";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeGuruReviewerConfig config = new AmazonCodeGuruReviewerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeGuruReviewerClient client = new AmazonCodeGuruReviewerClient(creds, config);
            
            ListRepositoryAssociationsResponse resp = new ListRepositoryAssociationsResponse();
            do
            {
                ListRepositoryAssociationsRequest req = new ListRepositoryAssociationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListRepositoryAssociationsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.RepositoryAssociationSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}