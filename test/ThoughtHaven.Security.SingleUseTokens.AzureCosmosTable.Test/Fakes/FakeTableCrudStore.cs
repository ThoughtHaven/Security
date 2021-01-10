using System;
using System.Threading.Tasks;
using ThoughtHaven.Data;
using ThoughtHaven.Security.SingleUseTokens.AzureCosmosTable;

namespace ThoughtHaven.Security.SingleUseTokens.Fakes
{
    public class FakeTableCrudStore : TableCrudStore<SingleUseToken, SingleUseTokenRecord>
    {
        public FakeTableCrudStore()
            : base(new FakeTableEntityStore(), t => null!, d => null!)
        { }

        public SingleUseToken? Retrieve_InputToken;
        public SingleUseTokenRecord? Retrieve_Output = new SingleUseTokenRecord()
        { Value = "value", Expiration = DateTimeOffset.UtcNow };
        public override Task<SingleUseTokenRecord?> Retrieve(SingleUseToken token)
        {
            this.Retrieve_InputToken = token;

            return Task.FromResult<SingleUseTokenRecord?>(this.Retrieve_Output);
        }

        public SingleUseTokenRecord? Create_InputModel;
        public override Task<SingleUseTokenRecord> Create(SingleUseTokenRecord model)
        {
            this.Create_InputModel = model;

            return Task.FromResult(model);
        }

        public SingleUseToken? Delete_InputToken;
        public override Task Delete(SingleUseToken token)
        {
            this.Delete_InputToken = token;

            return Task.FromResult(this.Delete_InputToken);
        }
    }
}