namespace ThoughtHaven.Security.SingleUseTokens
{
    public class SingleUseToken : ValueObject<string>
    {
        public SingleUseToken(string value) : base(value)
        {
            Guard.NullOrWhiteSpace(nameof(value), value);
        }
    }
}