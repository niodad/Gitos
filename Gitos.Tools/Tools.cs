using Newtonsoft.Json;

namespace Gitos.Tools
{
    public static class Tools
    {
        public static string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static T DeSerialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static string ToEuroString(this decimal d)
        {

            return d.ToString("C");
        }
    }
}