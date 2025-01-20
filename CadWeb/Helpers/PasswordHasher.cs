using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace CadWeb.Helpers
{
    public class PasswordHasher
    {
        private const int SaltSize = 16; // Tamanho do salt em bytes (128 bits)
        private const int HashSize = 32; // Tamanho do hash em bytes (256 bits)
        private const int Iterations = 100_000; // Número de iterações para PBKDF2

        /// <summary>
        /// Gera um salt aleatório de 128 bits.
        /// </summary>
        /// <returns>Salt como string Base64.</returns>
        public static string GenerateSalt()
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Gera o hash de uma senha usando PBKDF2 com HMACSHA256.
        /// </summary>
        /// <param name="password">Senha em texto claro.</param>
        /// <param name="salt">Salt em formato Base64.</param>
        /// <returns>Hash da senha em Base64.</returns>
        public static string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] hashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verifica se uma senha corresponde ao hash armazenado.
        /// </summary>
        /// <param name="password">Senha em texto claro.</param>
        /// <param name="storedHash">Hash armazenado em Base64.</param>
        /// <param name="salt">Salt usado para gerar o hash, em Base64.</param>
        /// <returns>Verdadeiro se a senha for válida; caso contrário, falso.</returns>
        public static bool VerifyPassword(string password, string storedHash, string salt)
        {
            string newHash = HashPassword(password, salt);
            return newHash == storedHash;
        }
    }
}