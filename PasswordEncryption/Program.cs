using PasswordEncryption;
using System.Text;

namespace Ecryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string Password = "123asd123";

            string saltKey = CreateHash.SaltKey(5);
            string passwordEncryption = CreateHash.Create(Encoding.UTF8.GetBytes(string.Concat(Password, saltKey)), TypeHashes.SHA512);

            Console.WriteLine($"Senha: {Password}, saltKey: {saltKey}, Criptografada: {passwordEncryption}");





            string CheckEncryption = CreateHash.ToCheckEncryption(Password, saltKey, TypeHashes.SHA512);

            Console.WriteLine("Senhas são iguas? " + passwordEncryption.Equals(CheckEncryption));
        }
    }
}
