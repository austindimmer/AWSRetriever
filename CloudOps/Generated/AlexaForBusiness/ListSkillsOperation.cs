using Amazon;
using Amazon.AlexaForBusiness;
using Amazon.AlexaForBusiness.Model;
using Amazon.Runtime;

namespace CloudOps.AlexaForBusiness
{
    public class ListSkillsOperation : Operation
    {
        public override string Name => "ListSkills";

        public override string Description => "Lists all enabled skills in a specific skill group.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "AlexaForBusiness";

        public override string ServiceID => "Alexa For Business";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAlexaForBusinessConfig config = new AmazonAlexaForBusinessConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAlexaForBusinessClient client = new AmazonAlexaForBusinessClient(creds, config);
            
            ListSkillsResponse resp = new ListSkillsResponse();
            do
            {
                ListSkillsRequest req = new ListSkillsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListSkillsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SkillSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}