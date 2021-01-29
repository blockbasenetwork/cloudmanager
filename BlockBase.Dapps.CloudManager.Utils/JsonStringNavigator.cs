using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Utils
{
    public static class JsonStringNavigator
    {
        public static string GetDeeper(string jsonString, string Field)
        {
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
            return jsonObj[Field].ToString();
        }

       
    }
}
