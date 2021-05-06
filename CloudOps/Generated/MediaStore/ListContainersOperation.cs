using Amazon;
using Amazon.MediaStore;
using Amazon.MediaStore.Model;
using Amazon.Runtime;

namespace CloudOps.MediaStore
{
    public class ListContainersOperation : Operation
    {
        public override string Name => "ListContainers";

        public override string Description => "Lists the properties of all containers in AWS Elemental MediaStore.  You can query to receive all the containers in one response. Or you can include the MaxResults parameter to receive a limited number of containers in each response. In this case, the response includes a token. To get the next set of containers, send the command again, this time with the NextToken parameter (with the returned token as its value). The next set of responses appears, with a token if there are still more containers to receive.  See also DescribeContainer, which gets the properties of one container. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "MediaStore";

        public override string ServiceID => "MediaStore";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaStoreConfig config = new AmazonMediaStoreConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaStoreClient client = new AmazonMediaStoreClient(creds, config);
            
            ListContainersResponse resp = new ListContainersResponse();
            do
            {
                ListContainersRequest req = new ListContainersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListContainersAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Containers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}