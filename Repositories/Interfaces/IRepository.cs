using System.Linq.Expressions;

namespace EmployeeDirectoryProject.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllPaged(int pageNumber, int pageSize);
        IQueryable<T> GetAllByQuery();
        Task<int> SaveChangingAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
