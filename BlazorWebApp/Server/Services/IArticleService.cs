using BlazorWebApp.Helpers;
using BlazorWebApp.Shared;

namespace BlazorWebApp.Server.Services
{
    public interface IArticleService
    {
        public void Add(Article article);

        public Task Save();

        public Task<Article> Find(int id);

        public List<Article> FindAll();

        public PagedList<Article> FindAll(ArticleParameters articleParameters);

        public Task<Article> Update(Article Article);

        public void Delete(Article Article);

        public bool ArticleExists(int id);
    }
}
