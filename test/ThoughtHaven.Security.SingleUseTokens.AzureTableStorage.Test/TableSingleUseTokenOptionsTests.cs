using System;
using Microsoft.WindowsAzure.Storage.Table;
using Xunit;

namespace ThoughtHaven.Security.SingleUseTokens
{
    public class TableSingleUseTokenOptionsTests
    {
        public class TableNameProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ReturnsSingleUseTokens()
                {
                    var options = new TableSingleUseTokenOptions();

                    Assert.Equal("SingleUseTokens", options.TableName);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void SetToNull_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new TableSingleUseTokenOptions().TableName = null;
                    });
                }

                [Fact]
                public void SetToEmpty_Throws()
                {
                    Assert.Throws<ArgumentException>("value", () =>
                    {
                        new TableSingleUseTokenOptions().TableName = "";
                    });
                }

                [Fact]
                public void SetToWhiteSpace_Throws()
                {
                    Assert.Throws<ArgumentException>("value", () =>
                    {
                        new TableSingleUseTokenOptions().TableName = " ";
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var options = new TableSingleUseTokenOptions { TableName = "Table" };

                    Assert.Equal("Table", options.TableName);
                }
            }
        }

        public class TableRequestProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ReturnsNotNull()
                {
                    var options = new TableSingleUseTokenOptions();

                    Assert.NotNull(options.TableRequest);
                }
            }

            public class SetAccessor
            {
                [Fact]
                public void SetToNull_Throws()
                {
                    Assert.Throws<ArgumentNullException>("value", () =>
                    {
                        new TableSingleUseTokenOptions().TableRequest = null;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var request = new TableRequestOptions();

                    var options = new TableSingleUseTokenOptions { TableRequest = request };

                    Assert.Equal(request, options.TableRequest);
                }
            }
        }
    }
}