using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace searchfight
{
    public class QueryProcessor
    {
        private string baseUri;
        private string apiKey;
        private string cx;
        private string query;
        private string url;

        public QueryProcessor(string baseUri, string key, string cx, string query)
        {
            this.baseUri = baseUri;
            this.apiKey = key;
            this.cx = cx;
            this.query = query;
        }

        public async Task<long> GetResults()
        {
            url = this.baseUri + "key=" + apiKey + "&cx=" + this.cx + "&q=" + this.query;
            long totalResults = 0; //stores the sum of estimated results per set of pages

            try
            {
                for (int i = 1; i < 100;) //max result set = 100 pages (using google custom search engine)
                {
                    //Paging result sets
                    using (HttpResponseMessage response = await RestClient.apiClient.GetAsync(url + "&start=" + i))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            RequestMetadata oRequestMetadata = await response.Content.ReadAsAsync<RequestMetadata>();
                            long numResults = Convert.ToInt64(oRequestMetadata.Queries.Request[0].TotalResults);
                            if (numResults == 0) break;
                            totalResults += numResults;
                        }
                        else
                        {
                            if (response.ReasonPhrase.Equals("Forbidden", StringComparison.InvariantCultureIgnoreCase))
                            {
                                Console.WriteLine("\nAPI query limit reached (100 search queries per day for free).");
                            }

                            throw new Exception(response.ReasonPhrase);
                        }
                    }

                    i += 10;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return totalResults;
        }
    }
}
