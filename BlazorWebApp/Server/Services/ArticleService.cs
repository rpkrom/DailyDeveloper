using BlazorWebApp.Helpers;
using BlazorWebApp.Server.Data;
using BlazorWebApp.Shared;

namespace BlazorWebApp.Server.Services
{
    public class ArticleService : IArticleService
    {
        private readonly DataContext _context;

        public ArticleService(DataContext context)
        {
            _context = context;
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }

        public void Add(Article movie)
        {
            _context.Add(movie);
        }

        public async Task<Article> Find(int id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public List<Article> FindAll()
        {
            return _context.Articles.OrderBy(m => m.Title).ToList();
        }

        public PagedList<Article> FindAll(ArticleParameters movieParameters)
        {
            return PagedList<Article>.ToPagedList(FindAll(), movieParameters.PageNumber, movieParameters.PageSize);

            //return  _context.Movies.OrderBy(m => m.Name)
            //   .Skip((movieParameters.PageNumber - 1) * movieParameters.PageSize)
            //   .Take(movieParameters.PageSize)
            //   .ToListAsync();            
        }

        public async Task<Article> Update(Article movie)
        {
            _context.Entry(movie).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return movie;
        }

        public void Delete(Article movie)
        {
            _context.Remove(movie);
        }

        public bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
       
    }
}
