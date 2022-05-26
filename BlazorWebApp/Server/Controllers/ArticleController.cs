using BlazorWebApp.Server.Services;
using BlazorWebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlazorWebApp.Server.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET: api/Articles
        [HttpGet]
        public ActionResult<IEnumerable<Article>> GetArticles([FromQuery] ArticleParameters articleParameters)
        {
            var articles = _articleService.FindAll(articleParameters);

            var metadata = new
            {
                articles.TotalCount,
                articles.PageSize,
                articles.CurrentPage,
                articles.HasNext,
                articles.HasPrevious
            };
            Response.Headers.Add("TestHeader", "TestContent"); // how to add headers
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(articles);
        }


        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _articleService.Find(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }


        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            try
            {
                await _articleService.Update(article);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!_articleService.ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex; // TODO: add logging here
                }
            }

            return NoContent();
        }

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            _articleService.Add(article);
            await _articleService.Save();

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        //PATCH: api/Articles/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchArticle(int id, JsonPatchDocument<Article> articleUpdates)
        {
            var article = await _articleService.Find(id);

            if (article == null)
            {
                return NotFound();
            }

            articleUpdates.ApplyTo(article);
            // this is how you patch 
            // "path": "/url",   // NAME of property
            // "op": "replace",       // Action to perform
            // "value": "yourNewUrl"  // New value
            await _articleService.Save();

            return NoContent();
        }


        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _articleService.Find(id);
            if (article == null || article.Id != id)
            {
                return NotFound();
            }

            _articleService.Delete(article);
            await _articleService.Save();

            return NoContent();
        }
    }
}
