namespace Davisoft_BDSProject.Domain.Helpers.Encryption
{
    public interface IStringEncryptor
    {
        string Encrypt(string data);
        string Decrypt(string data);
    }
}
