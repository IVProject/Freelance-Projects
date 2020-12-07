using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace InvsoftEngine.RemoteConfigManagement
{ 
    /// <summary>
    /// Responsible for requesting configuration from the server.
    /// </summary>
    public static class ConfigRequest
    {
        /// <summary>
        /// Called when the load completes and passes the response as an argument.
        /// </summary>
        public static Action<string> LoadCompleted;
        /// <summary>
        /// Called after a load fails and passes the error as an argument.
        /// </summary>
        public static Action<string> LoadFailed;

        public static async void LoadAsync(string url, int timeout = 0)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.timeout = timeout;
            await request.SendWebRequest();
            TryFetchResponse(request);
            request.Dispose();
        }

        public static IEnumerator Load(string url, int timeout = 0)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.timeout = timeout;
            yield return request.SendWebRequest();
            TryFetchResponse(request);
            request.Dispose();
        }

        private static void TryFetchResponse(UnityWebRequest request)
        {
            if (!request.isHttpError && !request.isNetworkError)
            {
                LoadCompleted?.Invoke(request.downloadHandler.text);
            }
            else
            {
                LoadFailed?.Invoke(request.error);
                Debug.LogErrorFormat("Error request [{0}, {1}]", request.url, request.error);
            }
        }
    }
}
