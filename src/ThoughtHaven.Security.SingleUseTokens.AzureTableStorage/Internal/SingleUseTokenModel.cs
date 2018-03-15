using System;

namespace ThoughtHaven.Security.SingleUseTokens.Internal
{
    public class SingleUseTokenModel
    {
        public string Value { get; set; }
        public DateTimeOffset Expiration { get; set; }

        public SingleUseTokenModel() { }

        public SingleUseTokenModel(SingleUseTokenRecord record) : this()
        {
            Guard.Null(nameof(record), record);

            this.Value = record.Value;
            this.Expiration = record.Expiration;
        }

        public SingleUseTokenRecord ToRecord() =>
            new SingleUseTokenRecord(this.Value, this.Expiration);
    }
}