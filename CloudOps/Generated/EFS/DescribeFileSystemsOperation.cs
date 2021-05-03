using Amazon;
using Amazon.EFS;
using Amazon.EFS.Model;
using Amazon.Runtime;

namespace CloudOps.EFS
{
    public class DescribeFileSystemsOperation : Operation
    {
        public override string Name => "DescribeFileSystems";

        public override string Description => "Returns the description of a specific Amazon EFS file system if either the file system CreationToken or the FileSystemId is provided. Otherwise, it returns descriptions of all file systems owned by the caller&#39;s AWS account in the AWS Region of the endpoint that you&#39;re calling. When retrieving all file system descriptions, you can optionally specify the MaxItems parameter to limit the number of descriptions in a response. Currently, this number is automatically set to 10. If more file system descriptions remain, Amazon EFS returns a NextMarker, an opaque token, in the response. In this case, you should send a subsequent request with the Marker request parameter set to the value of NextMarker.  To retrieve a list of your file system descriptions, this operation is used in an iterative process, where DescribeFileSystems is called first without the Marker and then the operation continues to call it with the Marker parameter set to the value of the NextMarker from the previous response until the response has no NextMarker.   The order of file systems returned in the response of one DescribeFileSystems call and the order of file systems returned across the responses of a multi-call iteration is unspecified.   This operation requires permissions for the elasticfilesystem:DescribeFileSystems action. ";
 
        public override string RequestURI => "/2015-02-01/file-systems";

        public override string Method => "GET";

        public override string ServiceName => "EFS";

        public override string ServiceID => "EFS";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEFSConfig config = new AmazonEFSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEFSClient client = new AmazonEFSClient(creds, config);
            
            DescribeFileSystemsResponse resp = new DescribeFileSystemsResponse();
            do
            {
                DescribeFileSystemsRequest req = new DescribeFileSystemsRequest
                {
                    Marker = resp.NextMarker
                    ,
                    MaxItems = maxItems
                                        
                };

                resp = client.DescribeFileSystems(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FileSystems)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}