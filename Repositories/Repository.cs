using Employee_Directory.DBContext;
using Employee_Directory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Linq.Expressions;

namespace Employee_Directory.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<int> SaveChangingAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAllPaged(int pageNumber,int pageSize)
        {
            return _context.Set<T>().ToPagedList(pageNumber,pageSize);
        }
        public IQueryable<T> GetAllByQuery()
        {
            return _context.Set<T>();
        }
    }
}
