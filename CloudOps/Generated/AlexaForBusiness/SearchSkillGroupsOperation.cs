using Amazon;
using Amazon.AlexaForBusiness;
using Amazon.AlexaForBusiness.Model;
using Amazon.Runtime;

namespace CloudOps.AlexaForBusiness
{
    public class SearchSkillGroupsOperation : Operation
    {
        public override string Name => "SearchSkillGroups";

        public override string Description => "Searches skill groups and lists the ones that meet a set of filter and sort criteria.";
 
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
            
            SearchSkillGroupsResponse resp = new SearchSkillGroupsResponse();
            do
            {
                SearchSkillGroupsRequest req = new SearchSkillGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.SearchSkillGroupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SkillGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}