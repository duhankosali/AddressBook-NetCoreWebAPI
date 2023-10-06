using System.Linq.Expressions;

namespace AddressBook.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id); // get by Id
        IQueryable<T> GetAll(); // get all
        IQueryable<T> Where(Expression<Func<T, bool>> predicate); // search
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);   
        Task AddAsync(T entity); // add
        void Update(T entity);  // update
        void Delete(T entity); // delete
    }
}
