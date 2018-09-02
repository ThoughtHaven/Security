using Microsoft.WindowsAzure.Storage.Table;
using System;
using Xunit;

namespace ThoughtHaven.Security.SingleUseTokens.AzureTableStorage
{
    public class TableSingleUseTokenConfigurationTests
    {
        public class TableNameProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ReturnsSingleUseTokens()
                {
                    var options = Configuration();

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
                        Configuration().TableName = null;
                    });
                }

                [Fact]
                public void SetToEmpty_Throws()
                {
                    Assert.Throws<ArgumentException>("value", () =>
                    {
                        Configuration().TableName = "";
                    });
                }

                [Fact]
                public void SetToWhiteSpace_Throws()
                {
                    Assert.Throws<ArgumentException>("value", () =>
                    {
                        Configuration().TableName = " ";
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var options = Configuration();
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
                    var options = Configuration();

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
                        Configuration().TableRequest = null;
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var request = new TableRequestOptions();
                    var options = Configuration();

                    options.TableRequest = request;

                    Assert.Equal(request, options.TableRequest);
                }
            }
        }

        public class Constructor
        {
            public class StorageAccountConnectionStringOverload
            {
                [Fact]
                public void NullStorageAccountConnectionString_Throws()
                {
                    Assert.Throws<ArgumentNullException>("storageAccountConnectionString", () =>
                    {
                        new TableSingleUseTokenConfiguration(
                            storageAccountConnectionString: null);
                    });
                }

                [Fact]
                public void EmptyStorageAccountConnectionString_Throws()
                {
                    Assert.Throws<ArgumentException>("storageAccountConnectionString", () =>
                    {
                        new TableSingleUseTokenConfiguration(
                            storageAccountConnectionString: "");
                    });
                }

                [Fact]
                public void WhiteSpaceStorageAccountConnectionString_Throws()
                {
                    Assert.Throws<ArgumentException>("storageAccountConnectionString", () =>
                    {
                        new TableSingleUseTokenConfiguration(
                            storageAccountConnectionString: " ");
                    });
                }

                [Fact]
                public void WhenCalled_SetsStorageAccountConnectionString()
                {
                    var config = new TableSingleUseTokenConfiguration(
                        "UseDevelopmentStorage=true;");

                    Assert.Equal("UseDevelopmentStorage=true;",
                        config.StorageAccountConnectionString);
                }
            }
        }

        private static TableSingleUseTokenConfiguration Configuration(
            string storageAccountConnectionString = null) =>
            new TableSingleUseTokenConfiguration(
                storageAccountConnectionString ?? "connectionString");
    }
}