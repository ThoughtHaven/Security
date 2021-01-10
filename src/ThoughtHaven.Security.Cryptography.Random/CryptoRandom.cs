using System;
using System.Security.Cryptography;

namespace ThoughtHaven.Security.Cryptography
{
    public class CryptoRandom
    {
        private const string Zero = "0";
        private const string Numbers = "1234567890";
        private static readonly long MaxValueLength = long.MaxValue.ToString().Length;

        public virtual long GenerateNumber(int length)
        {
            if (length > MaxValueLength)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(length),
                    message: $"The {nameof(length)} argument's value of {length} would require a number larger than the {nameof(Int64)}.{nameof(long.MaxValue)}.");
            }

            var randomString = this.GenerateString(length, Numbers);

            while (randomString.StartsWith(Zero))
            {
                var next = this.GenerateString(1, Numbers);
                randomString = $"{randomString[1..]}{next}";
            }

            return long.Parse(randomString);
        }

        public virtual string GenerateString(int length) =>
            this.GenerateString(length, characterSet: "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890");

        public virtual string GenerateString(int length, string characterSet)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(length), message: $"The {nameof(length)} argument cannot be less than 1.");
            }

            if (characterSet == null)
            {
                throw new ArgumentNullException(nameof(characterSet));
            }

            if (string.IsNullOrWhiteSpace(characterSet))
            {
                throw new ArgumentException(paramName: nameof(characterSet), message: $"The {nameof(characterSet)} argument cannot be null or white space.");
            }

            if (characterSet.Length < 2)
            {
                throw new ArgumentException(paramName: nameof(characterSet), message: $"The {nameof(characterSet)}.{nameof(characterSet.Length)} must be at least 2.");
            }

            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[length];

            rng.GetBytes(bytes);

            var result = new char[length];
            while (length-- > 0)
            {
                result[length] = characterSet[bytes[length] % characterSet.Length];
            }

            return new string(result);
        }
    }
}