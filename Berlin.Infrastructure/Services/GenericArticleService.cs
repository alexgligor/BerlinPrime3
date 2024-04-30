using Berlin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace Berlin.Infrastructure.Services
{
    public class GenericArticleService : IGenericService<GenericArticle>
    {
        private  SqlDbContext _db;
        public GenericArticleService(SqlDbContext db) 
        {
            _db = db;
            _db.Database.EnsureCreated();
        }
        public async Task<GenericArticle> Add(GenericArticle article)
        {
            article.CreateDate = DateTime.Now;
            article.UpdateDate = DateTime.Now;
            _db.GenericArticles.Add(article);
            await _db.SaveChangesAsync();
            return article;
        }

        public async Task<List<GenericArticle>> GetAll()
        {
            var articles = await _db.GenericArticles.OrderBy(article => article.Title).ToListAsync();
            return articles;
        }

        public async Task<GenericArticle> Remove(GenericArticle article)
        {
            _db.GenericArticles.Remove(article);

            await _db.SaveChangesAsync();
            return article;
        }
        public async Task<GenericArticle> Remove(int id)
        {
            var article = await _db.GenericArticles.FindAsync(id);
            if (article != null)
            {
                return null;
            }
            _db.GenericArticles.Remove(article);

            await _db.SaveChangesAsync();
            return article;
        }

        public async Task<GenericArticle> Update(GenericArticle article)
        {
            var item = await _db.GenericArticles.FindAsync(article.Id);
            if (item == null)
                return null;
            item.Title = article.Title;
            item.Description = article.Description;
            item.Currency = article.Currency;
            item.Price = article.Price; 
            item.UpdateDate = DateTime.Now;
            item.Count = article.Count;

            await _db.SaveChangesAsync();
            return article;
        }

        public async Task<List<GenericArticle>> FindAll(string findStr)
        {
            return await _db.GenericArticles.Where(art => art.Location.ToLower().Contains(findStr.ToLower())
                                                        || art.Title.ToLower().Contains(findStr.ToLower())
                                                        || art.Description.ToLower().Contains(findStr.ToLower())
                                                        || art.Count.ToString().Contains(findStr.ToLower())
                                                        || art.Price.ToString().Contains(findStr.ToLower())).ToListAsync() ;
        }

        public Task<List<GenericArticle>> GetAll(params Expression<Func<GenericArticle, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<GenericArticle> Find(int id, params Expression<Func<GenericArticle, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<List<GenericArticle>> FindAll(Expression<Func<GenericArticle, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<GenericArticle>> FindAll(Expression<Func<GenericArticle, bool>> filter, params Expression<Func<GenericArticle, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<GenericArticle> Get(int id, params Expression<Func<GenericArticle, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }
    }
}
