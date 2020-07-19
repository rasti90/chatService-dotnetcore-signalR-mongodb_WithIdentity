using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ChatServer.API.Helper {
    public static class StringExtensionMethods {
        public static string GetCustomHashCode (this string input, byte[] salt) {
            string hashed = Convert.ToBase64String (KeyDerivation.Pbkdf2 (
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}