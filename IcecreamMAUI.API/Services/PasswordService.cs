using System.Security.Cryptography;
using System.Text;

namespace IcecreamMAUI.API.Services
{
    public class PasswordService
    {
        private const int SaltSize = 10;
        public (string salt, string hash) GenerateSaltAndHash(string plainPassword) 
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentNullException(nameof(plainPassword));

            var buffer = RandomNumberGenerator.GetBytes(SaltSize);
            var salt =  Convert.ToBase64String(buffer);
            var hashedPassword = GenerateHashedPassword(plainPassword, salt);

            return (salt, hashedPassword);
        }

        //retrieve the salt and hashed password from the db for that user
        //then generate the same password from plain password and salt 
        //and compare to see if it matches the hashed password
        public bool AreEqual(string plainPassword, string hashedPassword, string salt)
        {
            //difference between hashing and encryption: hashing is 1 way cannot be dehashed 
            //encryption is 2 way, can be decrypted
            var newHashPassword = GenerateHashedPassword(plainPassword,salt);
            return newHashPassword == hashedPassword;
        }

        private static string GenerateHashedPassword(string plainPassword, string salt)
        {
            //concat plain password and salt and store in a byte[]
            var bytes = Encoding.UTF8.GetBytes(plainPassword + salt);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
