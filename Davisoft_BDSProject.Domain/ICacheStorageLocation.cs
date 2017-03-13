namespace Davisoft_BDSProject.Domain
{
    public interface ICacheStorageLocation
    {
        T Get<T>(string name) where T : class;
        T Set<T>(string name, T data) where T : class;
        bool HasKey(string name);
    }
}
