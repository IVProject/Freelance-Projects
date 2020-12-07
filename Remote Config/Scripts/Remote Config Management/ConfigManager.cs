using System;
using UnityEngine;

namespace InvsoftEngine.RemoteConfigManagement
{
    public static class ConfigManager
    {
        public static Action FetchCompleted;

        private const string Key = "ConfigManager";
        private static ConfigStorage m_ConfigStorage = new ConfigStorage(Key);

        public static IConfigStorage storage { get => m_ConfigStorage; }

        public static void FetchConfig(string url, int timeout = 0)
        {
            ConfigRequest.LoadAsync(url, timeout);
            ConfigRequest.LoadCompleted += AcceptConfig;
            ConfigRequest.LoadFailed += (string err) => TryFetchCacheConfig();
        }

        private static void AcceptConfig(string json)
        {
            m_ConfigStorage.SetConfig(json);
            FetchCompleted?.Invoke();
            ConfigRequest.LoadCompleted -= AcceptConfig;
        }

        private static void TryFetchCacheConfig()
        {
            if (m_ConfigStorage.HasKey(Key))
                FetchCompleted?.Invoke();
            else
                Debug.LogError("The cached configuration was not found");

            ConfigRequest.LoadFailed -= (string err)=>TryFetchCacheConfig();
        }
    }
}
