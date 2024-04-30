using Berlin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Berlin.Infrastructure.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseDbObject, new()
    {
        private readonly SqlDbContext _db;

        public GenericService(SqlDbContext db)
        {
            _db = db;
            _db.Database.EnsureCreated();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            // Presupunând că fiecare entitate are proprietățile CreateDate și UpdateDate
            ((dynamic)entity).CreateDate = DateTime.Now;
            ((dynamic)entity).UpdateDate = DateTime.Now;

            _db.Set<TEntity>().Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _db.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Presupunând că `Id` este numele proprietății după care vrei să ordonezi
            query = query.OrderByDescending(x => x.Id);

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _db.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            // Presupunând că `Id` este numele proprietății după care vrei să ordonezi
            query = query.OrderByDescending(x => x.Id);

            return await query.ToListAsync();
        }
        public Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _db.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            query = query.OrderByDescending(x => x.Id);

            return query.ToListAsync();
        }
        public async Task<TEntity> Remove(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Remove(int id)
        {
            var entity = await _db.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var entityToUpdate = await _db.Set<TEntity>().FindAsync(((dynamic)entity).Id);
            if (entityToUpdate == null)
                return null;

            _db.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            ((dynamic)entityToUpdate).UpdateDate = DateTime.Now; 

            await _db.SaveChangesAsync();
            return entity;
        }

        // Această metodă necesită ajustare pentru a fi generică; folosirea directă de proprietăți specifice nu va funcționa fără reflecție sau expresii dinamice
        public async Task<List<TEntity>> FindAll(string findStr)
        {
            // Implementația specifică depinde de structura entităților tale și poate necesita reflecție sau alte tehnici pentru a fi complet generică
            throw new NotImplementedException("Implementația specifică depinde de structura entităților tale.");
        }

        public async Task<TEntity> Find(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID must be positive.", nameof(id));
            }

            if (includeProperties == null)
            {
                throw new ArgumentNullException(nameof(includeProperties));
            }

            try
            {
                IQueryable<TEntity> query = _db.Set<TEntity>();

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                var stat = _db.Database.GetDbConnection().State;
                TEntity entity = await query.FirstOrDefaultAsync(e => ((BaseDbObject)e).Id == id);

                return entity;
            }
            catch (Exception ex)
            {
                throw; 
            }
        }

    }
}
