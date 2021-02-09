

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Utils
{
    public static class Fetch
    {

        public static async Task<string> GetAsync(string uri)
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

        public static async Task<string> PostAsync(string uri, string body = "")
        {
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsync(uri, content))
                {
                    using (HttpContent resp = res.Content)
                    {
                        return await resp.ReadAsStringAsync();

                    }
                }
            }
        }
    }
}
