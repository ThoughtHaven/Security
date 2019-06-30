using System;
using Xunit;

namespace ThoughtHaven.Security.Cryptography
{
    public class CryptoRandomTests
    {
        private static readonly Random _random = new Random();

        public class GenerateNumberMethod
        {
            public class LengthOverload
            {
                [Fact]
                public void ValidLength_ReturnsIntOfCorrectLength()
                {
                    var length = _random.Next(3, long.MaxValue.ToString().Length);
                    var random = new CryptoRandom();

                    var result = random.GenerateNumber(length);

                    Assert.Equal(length, result.ToString().Length);
                }

                [Fact]
                public void LengthLessThanOne_Throws()
                {
                    var random = new CryptoRandom();

                    Assert.Throws<ArgumentOutOfRangeException>("length", () =>
                    {
                        random.GenerateNumber(length: 0);
                    });
                }

                [Fact]
                public void LengthLargerThanInt64MaxValue_Throws()
                {
                    var random = new CryptoRandom();

                    Assert.Throws<ArgumentOutOfRangeException>("length", () =>
                    {
                        random.GenerateNumber(length: long.MaxValue.ToString().Length + 1);
                    });
                }
            }
        }

        public class GenerateString
        {
            public class LengthOverload
            {
                [Fact]
                public void ValidLength_ReturnsStringOfCorrectLength()
                {
                    var length = _random.Next(5, 100);
                    var random = new CryptoRandom();

                    var result = random.GenerateString(length);

                    Assert.Equal(length, result.Length);
                }

                [Fact]
                public void LengthLessThanOne_Throws()
                {
                    var random = new CryptoRandom();

                    Assert.Throws<ArgumentOutOfRangeException>("length", () =>
                    {
                        random.GenerateString(length: 0);
                    });
                }
            }

            public class LengthAndCharacterSetOverload
            {
                [Fact]
                public void ValidArgs_ReturnsStringOfCorrectLengthWithExpectedCharacters()
                {
                    var length = _random.Next(5, 100);
                    var characterSet = "anpz370";
                    var random = new CryptoRandom();

                    var result = random.GenerateString(length, characterSet);

                    Assert.Equal(length, result.Length);

                    foreach (var character in result)
                    {
                        Assert.Contains(character.ToString(), characterSet);
                    }
                }

                [Fact]
                public void LengthLessThanOne_Throws()
                {
                    var random = new CryptoRandom();

                    Assert.Throws<ArgumentOutOfRangeException>("length", () =>
                    {
                        random.GenerateString(length: 0, characterSet: "abc");
                    });
                }

                [Fact]
                public void NullCharacterSet_Throws()
                {
                    var random = new CryptoRandom();

                    Assert.Throws<ArgumentNullException>("characterSet", () =>
                    {
                        random.GenerateString(length: 5, characterSet: null!);
                    });
                }

                [Fact]
                public void EmptyCharacterSet_Throws()
                {
                    var random = new CryptoRandom();

                    Assert.Throws<ArgumentException>("characterSet", () =>
                    {
                        random.GenerateString(length: 5, characterSet: string.Empty);
                    });
                }

                [Fact]
                public void WhiteSpaceCharacterSet_Throws()
                {
                    var random = new CryptoRandom();
                    
                    Assert.Throws<ArgumentException>("characterSet", () =>
                    {
                        random.GenerateString(length: 5, characterSet: " ");
                    });
                }

                [Fact]
                public void CharacterSetUnderTwoCharactersLong_Throws()
                {
                    var random = new CryptoRandom();

                    Assert.Throws<ArgumentException>("characterSet", () =>
                    {
                        random.GenerateString(length: 5, characterSet: "a");
                    });
                }
            }
        }
    }
}