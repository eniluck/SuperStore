using SuperStore.Shared;

namespace SuperStore.Funds.Messages
{
    public record FundsMessage(long CustomerID, decimal CurrentFunds) : IMessage;
}
