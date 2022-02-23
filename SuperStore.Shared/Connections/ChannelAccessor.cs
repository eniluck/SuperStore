using RabbitMQ.Client;

namespace SuperStore.Shared.Connections
{
    /// <summary>
    /// Allows to access particular channel depending on the currently using thread.
    /// Same idea from: https://github.com/aspnet/HttpAbstractions/blob/master/src/Microsoft.AspNetCore.Http/HttpContextAccessor.cs
    /// </summary>
    internal sealed class ChannelAccessor
    {
        // ThreadLocal = struct to store Value per thread
        private static readonly ThreadLocal<ChannelHolder> Holder = new();

        public IModel? Channel
        {
            get => Holder.Value?.Context;
            set
            {
                var holder = Holder.Value;
                if (holder is not null)
                {
                    holder.Context = null;
                }

                if (value is not null)
                {
                    Holder.Value = new ChannelHolder { Context = value };
                }
            }
        }

        private class ChannelHolder
        {
            public IModel? Context;
        }
    }
}
