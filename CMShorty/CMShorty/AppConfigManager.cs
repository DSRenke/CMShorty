namespace CMShorty
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    public class AppConfigManager
    {
        private const string Namespace = "CMShorty";
        private const string Filename = "AppConfig.json";

        private JObject _secrets;

        public AppConfigManager()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppConfigManager)).Assembly;
            var stream = assembly.GetManifestResourceStream($"{Namespace}.{Filename}");
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                _secrets = JObject.Parse(json);
            }
        }

        public string Get(string name)
        {
            try
            {
                var path = name.Split(':');

                JToken node = _secrets[path[0]];
                for (int index = 1; index < path.Length; index++)
                {
                    node = node[path[index]];
                }

                return node.ToString();
            }
            catch (Exception)
            {
                Debug.WriteLine($"Unable to retrieve secret '{name}'");
                return string.Empty;
            }
        }
    }
}
