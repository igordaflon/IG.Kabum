using KBM.Core.DomainObjects;

namespace KBM.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
}
