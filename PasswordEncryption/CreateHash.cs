using System.Security.Cryptography;
using System.Text;

namespace PasswordEncryption
{
    public class CreateHash
    {
        public static string SaltKey(int size)
        {
            //generate a cryptographic random number
            var provider = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            provider.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }


        //Create a data hash
        public static string Create(byte[] data, TypeHashes hashAlgorithm, int trimByteCount = 0)
        {
            if (string.IsNullOrEmpty(hashAlgorithm.ToString()))
                throw new ArgumentNullException(nameof(hashAlgorithm));

            var algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(hashAlgorithm.ToString());
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            if (trimByteCount > 0 && data.Length > trimByteCount)
            {
                var newData = new byte[trimByteCount];
                Array.Copy(data, newData, trimByteCount);

                return BitConverter.ToString(algorithm.ComputeHash(newData)).Replace("-", string.Empty);
            }

            return BitConverter.ToString(algorithm.ComputeHash(data)).Replace("-", string.Empty);
        }

        public static string ToCheckEncryption(string password, string saltkey, TypeHashes passwordFormat)
        {
            return Create(Encoding.UTF8.GetBytes(String.Concat(password, saltkey)), passwordFormat);
        }
    }
}
