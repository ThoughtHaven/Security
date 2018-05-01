using System;
using System.Threading.Tasks;
using ThoughtHaven.Data;
using ThoughtHaven.Security.SingleUseTokens.AzureTableStorage;

namespace ThoughtHaven.Security.SingleUseTokens.Fakes
{
    public class FakeTableCrudStore : TableCrudStore<SingleUseToken, SingleUseTokenModel>
    {
        public FakeTableCrudStore() : base(new FakeTableEntityStore(), t => null, d => null) { }

        public SingleUseToken Retrieve_InputToken;
        public SingleUseTokenModel Retrieve_Output = new SingleUseTokenModel()
        { Value = "value", Expiration = DateTimeOffset.UtcNow };
        public override Task<SingleUseTokenModel> Retrieve(SingleUseToken token)
        {
            this.Retrieve_InputToken = token;

            return Task.FromResult(this.Retrieve_Output);
        }

        public SingleUseTokenModel Create_InputModel;
        public override Task<SingleUseTokenModel> Create(SingleUseTokenModel model)
        {
            this.Create_InputModel = model;

            return Task.FromResult(model);
        }

        public SingleUseToken Delete_InputToken;
        public override Task Delete(SingleUseToken token)
        {
            this.Delete_InputToken = token;

            return Task.FromResult(this.Delete_InputToken);
        }
    }
}