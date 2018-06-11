using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ApiCore.BO;
using ApiCore.DB;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Article = ApiCore.DTO.Article;
using Category = ApiCore.BO.Category;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// Выбираем категории для пользователя.
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        [Authorize]
        [Route("categories")]
        [HttpPost]
        public IActionResult Categories([FromBody] List<string> categories)
        {
            using (var db = new PeerDb())
            {
                var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var user = db.Users
                    .FirstOrDefault(u => u.Name == name);
                var boCategories = new List<Category>();
                foreach (var category in categories)
                {
                    var c = db.Categories.FirstOrDefault(cat => cat.Name == category);
                    if (c != null)
                        boCategories.Add(c);
                    else
                    {
                        BadRequest($"Category {category} doesn't exist");
                    }
                }

                db.CategoryToUserBindings.Where(c => c.UserId == user.Id).Delete();
                foreach (var category in boCategories)
                {
                    db.Insert(new CategoryToUserBinding(user.Id, category.Id));
                }

                return Ok();
            }
        }

        /// <summary>
        /// Создаем статью.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("article")]
        public IActionResult Article([FromBody] Article article)
        {
            if (string.IsNullOrEmpty(article.Title))
                return BadRequest("Title must be set");
            if (string.IsNullOrEmpty(article.Text))
                return BadRequest("Text must be set");
            using (var db = new PeerDb())
            {
                var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var user = db.Users
                    .FirstOrDefault(u => u.Name == name);
                article.AuthorId = user.Id;
                article.IsAccepted = null;
                var boArticle = new BO.Article(article);
                db.Insert(boArticle);
                var specialistIds = db.CategoryToUserBindings
                    .Where(ctub => ctub.CategoryId == article.CategoryId && ctub.UserId != user.Id)
                    .Select(x => x.UserId).ToList();
                var random = new Random();
                var reviewersCount = Math.Min(random.Next(1, 5), specialistIds.Count);
                if (reviewersCount != 0)
                {
                    var deadLine = DateTime.Now.AddDays(1);
                    var addedSpecialists = new List<Guid>();
                    while (addedSpecialists.Count != reviewersCount)
                    {
                        var index = random.Next(0, specialistIds.Count);
                        var specialistId = specialistIds[index];
                        addedSpecialists.Add(specialistId);
                        specialistIds.Remove(specialistId);
                    }

                    foreach (var sId in addedSpecialists)
                    {
                        db.Insert(new ArticleToUserBinding(sId, boArticle.Id, deadLine));
                    }
                }
                else
                {
                    boArticle.IsAccepted = true;
                    db.Update(boArticle);
                }

                return Ok($"{boArticle.Id}");
            }
        }

        [Authorize]
        [HttpPost("reject/{id}")]
        public IActionResult RejectReview([FromRoute] Guid id)
        {
            try
            {
                using (var db = new PeerDb())
                {
                    var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    var user = db.Users
                        .FirstOrDefault(u => u.Name == name);

                    var atb = db.ArticleToUserBindings
                        .Where(a => a.ArticleId == id && a.UserId == user.Id);
                    if (!atb.Any())
                        return BadRequest("You have not access");
                    atb.Set(a => a.Accepted, false)
                        .Update();
                    var article = db.Articles.Single(a => a.Id == id);
                    var reviewers = db.ArticleToUserBindings.Where(a => a.ArticleId == id && a.UserId != user.Id)
                        .Select(r => r.UserId).ToList();
                    var newReviewer = db.CategoryToUserBindings
                        .FirstOrDefault(b =>
                            b.CategoryId == article.CategoryId && b.UserId != user.Id &&
                            !reviewers.Contains(b.UserId) && article.UserId != b.UserId);
                    if (newReviewer != null)
                        db.Insert(new ArticleToUserBinding(newReviewer.UserId, article.Id,
                            DateTime.Now.AddDays(1)));
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Smth is wrong");
            }
        }

        [Authorize]
        [HttpPost("approve/{id}/{mark}")]
        public IActionResult ApproveReview([FromRoute] Guid id, int mark)
        {
            if (mark < 1 && mark > 5)
                return BadRequest("mark can be from 1 to 5");
            try
            {
                using (var db = new PeerDb())
                {
                    var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    var user = db.Users
                        .FirstOrDefault(u => u.Name == name);

                    var atb = db.ArticleToUserBindings
                        .Where(a => a.ArticleId == id && a.UserId == user.Id);
                    if (!atb.Any())
                        return BadRequest("You have not access");
                    atb.Set(a => a.Accepted, true)
                        .Update();
                    db.Insert(new Review(user.Id, mark, id));
                    if (db.ArticleToUserBindings.Where(a => a.ArticleId == id).All(a => a.Accepted == true))
                        db.Articles.Where(a => a.Id == id).Set(a => a.IsAccepted, true).Update();
                    user.Rating += 1;
                    db.Update(user);
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Smth is wrong");
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Article article)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}