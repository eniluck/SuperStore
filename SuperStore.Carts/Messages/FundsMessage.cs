using SuperStore.Shared;

namespace SuperStore.Carts.Messages
{
    public record FundsMessage(long CustomerID, decimal CurrentFunds) : IMessage;
}
