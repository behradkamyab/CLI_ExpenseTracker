namespace ExpenseTracker.Interfaces
{
    public interface IService<T>
    {
         void Add(T entity);
         void Remove(T entity);
         void Save();
         IEnumerable<T> Load();
    }
}
