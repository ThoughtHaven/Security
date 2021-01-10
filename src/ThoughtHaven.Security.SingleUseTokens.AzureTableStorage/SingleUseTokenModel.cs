﻿using System;
using ThoughtHaven.Security.SingleUseTokens.Internal;

namespace ThoughtHaven.Security.SingleUseTokens.AzureTableStorage
{
    public class SingleUseTokenModel
    {
        public string? Value { get; set; }
        public DateTimeOffset? Expiration { get; set; }

        public SingleUseTokenModel() { }

        public SingleUseTokenModel(SingleUseTokenData record)
            : this()
        {
            Guard.Null(nameof(record), record);

            this.Value = record.Value;
            this.Expiration = record.Expiration.ToOffset();
        }

        public SingleUseTokenData ToData() =>
            new SingleUseTokenData(this.Value!, new UtcDateTime(this.Expiration!.Value));
    }
}