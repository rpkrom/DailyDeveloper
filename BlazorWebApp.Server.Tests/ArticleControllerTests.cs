using BlazorWebApp.Server.Controllers;
using BlazorWebApp.Server.Services;
using BlazorWebApp.Shared;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BlazorWebApp.Server.Tests
{
    public class ArticleControllerTests
    {
        [Fact]
        public async void Test1()
        {
            //arrange
            var data = A.Fake<IArticleService>();
            var fakeArticle = GetFakeArticles().FirstOrDefault();
            
            A.CallTo(() => data.Find(fakeArticle.Id)).Returns(fakeArticle);
            var articleController = new ArticleController(data);

            //act
            var actionResult = await articleController.GetArticle(fakeArticle.Id);

            var result = actionResult.Result as OkObjectResult;
            var returnArticle = result.Value as Article;

            //assert
            Assert.Equal(fakeArticle, returnArticle);
            Assert.NotNull(returnArticle);

        }

        // Fake Movies Setup
        private List<Article> GetFakeArticles()
        {
            var testArticles = new List<Article>();
            testArticles.Add(new Article { Id = 1, Url = "https://test.google.com", ImageURL = "somepath/tofile", Title = "Test 1", Description = "description" });
            testArticles.Add(new Article { Id = 1, Url = "https://test.google.com", ImageURL = "somepath/tofile", Title = "Test 2", Description = "description" });
            testArticles.Add(new Article { Id = 1, Url = "https://test.google.com", ImageURL = "somepath/tofile", Title = "Test 3", Description = "description" });
            testArticles.Add(new Article { Id = 1, Url = "https://test.google.com", ImageURL = "somepath/tofile", Title = "Test 4", Description = "description" });
            testArticles.Add(new Article { Id = 1, Url = "https://test.google.com", ImageURL = "somepath/tofile", Title = "Test 5", Description = "description" });
           
            return testArticles;
        }
    }
}