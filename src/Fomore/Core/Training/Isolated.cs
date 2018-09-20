using System;

namespace Core.Training
{
    public sealed class Isolated<T> : IDisposable where T : MarshalByRefObject
    {
        private AppDomain Domain { get; set; }

        public Isolated()
        {
            Domain = AppDomain.CreateDomain("Isolated:" + Guid.NewGuid(),
                                            null, AppDomain.CurrentDomain.SetupInformation);

            var type = typeof(T);

            Value = (T)Domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName ?? throw new InvalidOperationException());
        }

        public T Value { get; }

        public void Dispose()
        {
            if (Domain != null)
            {
                AppDomain.Unload(Domain);

                Domain = null;
            }
        }
    }
}