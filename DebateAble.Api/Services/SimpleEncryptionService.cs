using System.Security.Cryptography;
using System.Text;

namespace DebateAble.Api.Services
{

    public interface ISimpleEncryptionService
    {
        Task<string> Encrypt(string plainText);
        Task<string> Decrypt(string encryptText);
    }

    public class SimpleEncryptionService : ISimpleEncryptionService
    {
        private readonly IConfiguration _config;

        public SimpleEncryptionService(
            IConfiguration config
            )
        {
            _config = config;
        }


        public async Task<string> Decrypt(string encrypted)
        {
            if (encrypted == null)
            {
                throw new ArgumentNullException(nameof(encrypted));
            }

            var key = _config.GetValue<string>("SimpleEncryption:Key");
            var parts = encrypted.Split("!");
            if(parts.Length < 2)
            {
                throw new InvalidOperationException($"Encrypted data does is not in the correct format.");
            }

            var iv = Convert.FromBase64String( parts[0]);
            var cipherText = Convert.FromBase64String(parts[1]);

            using (SymmetricAlgorithm crypt = Aes.Create())
            using (HashAlgorithm hash = MD5.Create())
            using (MemoryStream memoryStream = new MemoryStream())
            {
                crypt.Key = hash.ComputeHash(Encoding.UTF8.GetBytes(key));

                using (CryptoStream cryptoStream = new CryptoStream(
                    memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    await cryptoStream.WriteAsync(cipherText, 0, cipherText.Length);
                }

                return await Task.FromResult(System.Text.Encoding.UTF8.GetString(memoryStream.ToArray()));
            }

        }

        public async Task<string> Encrypt(string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var key = _config.GetValue<string>("SimpleEncryption:Key");

            byte[] bytes = Encoding.UTF8.GetBytes(data);

            using (SymmetricAlgorithm crypt = Aes.Create())
            using (HashAlgorithm hash = MD5.Create())
            using (MemoryStream memoryStream = new MemoryStream())
            {
                crypt.Key = hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                // This is really only needed before you call CreateEncryptor the second time,
                // since it starts out random.  But it's here just to show it exists.
                crypt.GenerateIV();

                using (CryptoStream cryptoStream = new CryptoStream(
                    memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    await cryptoStream.WriteAsync(bytes, 0, bytes.Length);
                }

                string base64IV = Convert.ToBase64String(crypt.IV);
                string base64Ciphertext = Convert.ToBase64String(memoryStream.ToArray());

                return await Task.FromResult(base64IV + "!" + base64Ciphertext);
            }
        }
    }
}
