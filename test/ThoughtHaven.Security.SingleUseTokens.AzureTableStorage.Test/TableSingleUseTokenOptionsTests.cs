using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage.Table;
using Xunit;

namespace ThoughtHaven.Security.SingleUseTokens.AzureTableStorage
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
                    var options = Options();

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
                        Options().TableName = null;
                    });
                }

                [Fact]
                public void SetToEmpty_Throws()
                {
                    Assert.Throws<ArgumentException>("value", () =>
                    {
                        Options().TableName = "";
                    });
                }

                [Fact]
                public void SetToWhiteSpace_Throws()
                {
                    Assert.Throws<ArgumentException>("value", () =>
                    {
                        Options().TableName = " ";
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var options = Options();
                    options.TableName = "Table";

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
                    var options = Options();

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
                        Options().TableRequest = null;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var request = new TableRequestOptions();
                    var options = Options();

                    options.TableRequest = request;

                    Assert.Equal(request, options.TableRequest);
                }
            }
        }

        private static TableSingleUseTokenOptions Options(
            string storageAccountConnectionString = null) =>
            new TableSingleUseTokenOptions(
                storageAccountConnectionString ?? "connectionString");
    }
}