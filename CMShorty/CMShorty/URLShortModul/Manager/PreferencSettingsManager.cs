namespace CMShorty.URLShortModul.Manager
{
    using Newtonsoft.Json;
    using Xamarin.Essentials;

    public class PreferencSettingsManager : ISettingsManager
    {
        public bool Contains(string key)
        {
            return Preferences.ContainsKey(key);
        }

        public TValue Get<TValue>(string key)
        {
            if (typeof(TValue) == typeof(string))
            {
                return (TValue)(object)Preferences.Get(key, string.Empty);
            }

            if (typeof(TValue) == typeof(int))
            {
                return (TValue)(object)Preferences.Get(key, 0);
            }

            if (typeof(TValue) == typeof(bool))
            {
                return (TValue)(object)Preferences.Get(key, false);
            }

            string serializedValue = Preferences.Get(key, string.Empty);

            if (!string.IsNullOrEmpty(serializedValue))
            {
                return JsonConvert.DeserializeObject<TValue>(serializedValue);
            }

            return default(TValue);
        }

        public void Remove(string key)
        {
            Preferences.Remove(key);
        }

        public void Set<TValue>(string key, TValue value)
        {
            if (typeof(TValue) == typeof(string))
            {
                Preferences.Set(key, (string)(object)value);
                return;
            }

            if (typeof(TValue) == typeof(int))
            {
                Preferences.Set(key, (int)(object)value);
                return;
            }

            if (typeof(TValue) == typeof(bool))
            {
                Preferences.Set(key, (bool)(object)value);
                return;
            }

            string serializedValue = JsonConvert.SerializeObject(value);
            Preferences.Set(key, serializedValue);
        }
    }
}
