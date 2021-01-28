

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Utils
{
    public static class Fetch
    {

        public static async Task<string> CallAsync(string uri)
        {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(uri))
                    {
                        using (HttpContent content = res.Content)
                        {
                            return await content.ReadAsStringAsync();
                            
                        }
                    }
                }   
        }
    }
}
