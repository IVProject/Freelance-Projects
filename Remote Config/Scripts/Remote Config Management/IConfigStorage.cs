
namespace InvsoftEngine.RemoteConfigManagement
{
    public interface IConfigStorage
    {
        T GetConfig<T>();
        void SetConfig(string json);
    }
}
