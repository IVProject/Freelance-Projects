using UnityEngine;

namespace InvsoftEngine.RemoteConfigManagement
{
    /// <summary>
    /// Responsible for storing the configuration.
    /// </summary>
    public sealed class ConfigStorage: IConfigStorage
    {
        private string m_Json;

        public string key { get; private set; }

        public ConfigStorage(string key)
        {
            this.key = key;
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public T GetConfig<T>()
        {
            if (m_Json == null)
                m_Json = PlayerPrefs.GetString(key);

            return JsonUtility.FromJson<T>(m_Json);
        }

        public void SetConfig(string json)
        {
            m_Json = json;
            SaveConfig(key, json);
        }

        public void SetConfig<T>(T config)
        {
            m_Json = JsonUtility.ToJson(config);
            SaveConfig(m_Json, key);
        }

        private void SaveConfig(string key, string json)
        {
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
    }
}
