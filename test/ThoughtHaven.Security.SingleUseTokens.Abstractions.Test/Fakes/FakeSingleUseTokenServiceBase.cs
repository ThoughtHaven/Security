﻿using System;
using System.Threading.Tasks;
using ThoughtHaven.Security.SingleUseTokens.Internal;

namespace ThoughtHaven.Security.SingleUseTokens.Fakes
{
    public class FakeSingleUseTokenServiceBase : SingleUseTokenServiceBase
    {
        public FakeSingleUseTokenServiceBase(FakeSystemClock clock) : base(clock) { }

        public string? Create_InputData_Value;
        public DateTimeOffset Create_InputData_Expiration;
        protected override Task Create(SingleUseTokenRecord data)
        {
            this.Create_InputData_Value = data.Value;
            this.Create_InputData_Expiration = data.Expiration;

            return Task.CompletedTask;
        }

        public string? Delete_InputData_Value;
        public DateTimeOffset Delete_InputData_Expiration;
        protected override Task Delete(SingleUseTokenRecord data)
        {
            this.Delete_InputData_Value = data.Value;
            this.Delete_InputData_Expiration = data.Expiration;

            return Task.CompletedTask;
        }

        public bool Retrieve_ShouldReturnData = true;
        public SingleUseToken? Retrieve_InputToken;
        public string? Retrieve_Output_Value;
        public DateTimeOffset Retrieve_Output_Expiration = DateTimeOffset.UtcNow;
        protected override Task<SingleUseTokenRecord?> Retrieve(SingleUseToken token)
        {
            this.Retrieve_InputToken = token;

            if (this.Retrieve_ShouldReturnData)
            {
                this.Retrieve_Output_Value = token.Value;

                return Task.FromResult<SingleUseTokenRecord?>(
                    new SingleUseTokenRecord(token, this.Retrieve_Output_Expiration));
            }

            return Task.FromResult<SingleUseTokenRecord?>(null);
        }
    }
}