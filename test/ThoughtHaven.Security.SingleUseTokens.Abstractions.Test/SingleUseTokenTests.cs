using System;
using Xunit;

namespace ThoughtHaven.Security.SingleUseTokens
{
    public class SingleUseTokenTests
    {
        public class Type
        {
            [Fact]
            public void InheritsValueObjectOfString()
            {
                Assert.True(typeof(ValueObject<string>).IsAssignableFrom(
                    typeof(SingleUseToken)));
            }
        }

        public class Constructor
        {
            public class ValueOverload
            {
                [Fact]
                public void NotNullValue_SetsValue()
                {
                    var value = Guid.NewGuid().ToString();

                    var token = new SingleUseToken(value);

                    Assert.Equal(value, token.Value);
                }

                [Fact]
                public void NullValue_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new SingleUseToken(value: null);
                    });
                }

                [Fact]
                public void EmptyValue_Throws()
                {
                    Assert.Throws<ArgumentException>("value", () =>
                    {
                        new SingleUseToken(value: string.Empty);
                    });
                }

                [Fact]
                public void WhiteSpaceValue_Throws()
                {
                    Assert.Throws<ArgumentException>("value", () =>
                    {
                        new SingleUseToken(value: " ");
                    });
                }
            }
        }

        public class SingleUseTokenOperator
        {
            public class ValueOverload
            {
                [Fact]
                public void WhenCalled_ReturnsToken()
                {
                    SingleUseToken token = "value";

                    Assert.Equal("value", token.Value);
                }
            }
        }
    }
}