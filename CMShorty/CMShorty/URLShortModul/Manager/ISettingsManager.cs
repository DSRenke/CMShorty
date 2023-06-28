namespace CMShorty.URLShortModul.Manager
{
    public interface ISettingsManager
    {
        TValue Get<TValue>(string key);

        void Set<TValue>(string key, TValue value);

        void Remove(string key);

        bool Contains(string key);
    }
}
