using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.CloudManager.Utils
{
    public static class JsonStringNavigator
    {
        public static String GetDeeper(string jsonString, string field)
        {
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
            return jsonObj[field].ToString();
        }

        public static T GetDeeper<T>(string jsonString, string field)
        {
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, Object>>(jsonString);
            return JsonConvert.DeserializeObject<T>(jsonObj[field].ToString());
        }

        public static T GetValue<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }




    }
}
