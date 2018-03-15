namespace ThoughtHaven.Security.SingleUseTokens
{
    public class SingleUseToken : ValueObject<string>
    {
        public SingleUseToken(string value) : base(value)
        {
            Guard.NullOrWhiteSpace(nameof(value), value);
        }

        public static implicit operator SingleUseToken(string value) =>
            new SingleUseToken(value);
    }
}