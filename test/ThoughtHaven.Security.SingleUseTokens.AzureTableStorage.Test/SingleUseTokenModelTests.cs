using System;
using ThoughtHaven.Security.SingleUseTokens.Internal;
using Xunit;

namespace ThoughtHaven.Security.SingleUseTokens.AzureTableStorage
{
    public class SingleUseTokenModelTests
    {
        public class Constructor
        {
            public class RecordOverload
            {
                [Fact]
                public void NullRecord_Throws()
                {
                    Assert.Throws<ArgumentNullException>("record", () =>
                    {
                        new SingleUseTokenModel(record: null!);
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var record = new SingleUseTokenRecord("value", DateTimeOffset.UtcNow);

                    var model = new SingleUseTokenModel(record);

                    Assert.Equal(record.Value, model.Value);
                }

                [Fact]
                public void WhenCalled_SetsExpiration()
                {
                    var record = new SingleUseTokenRecord("value", DateTimeOffset.UtcNow);

                    var model = new SingleUseTokenModel(record);

                    Assert.Equal(record.Expiration, model.Expiration);
                }
            }
        }

        public class ToRecordMethod
        {
            public class EmptyOverload
            {
                [Fact]
                public void WhenCalled_ReturnsRecord()
                {
                    var model = new SingleUseTokenModel(new SingleUseTokenRecord(
                        "value", DateTimeOffset.UtcNow));

                    var record = model.ToRecord();

                    Assert.Equal(model.Value, record.Value);
                    Assert.Equal(model.Expiration, record.Expiration);
                }
            }
        }
    }
}