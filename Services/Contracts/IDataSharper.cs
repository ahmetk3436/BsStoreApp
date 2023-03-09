using Entities.Models;

namespace Services.Contracts
{
    public interface IDataSharper<T>
    {
        IEnumerable<ShapedEntity> SharperData(IEnumerable<T> entities,string fieldsString);
        ShapedEntity SharperData(T entity, string fieldsString);
    }
}
