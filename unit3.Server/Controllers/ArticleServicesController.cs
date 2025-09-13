using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Data.Access.Library.Core;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;


namespace unit3.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleServicesController : ControllerBase
    {
        private readonly Settings _settings;

        public ArticleServicesController(Settings settings) { this._settings = settings; }

        [HttpGet()]
        public string GetHeartbeat()
        {
            return "Success";
        }

        [Authorize]
        [HttpGet("article")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult GetArticles()
        {
            try
            {
                string sql = "SELECT * FROM unit3.articles";
                List<Article> results = DataAccess.Instance.Select<Article>(_settings.ApiDatabase, sql);

                if (results == null || results.Count == 0)
                {
                    return NotFound("No articles found.");
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving articles: {ex.Message}");
            }
        }


        [HttpPost("article")]
        public IActionResult InsertArticles([FromQuery] string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return BadRequest("The 'json' query parameter is required.");
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                string sanitizedJson = json.Replace("'", "\"");

                List<Article> articles = JsonSerializer.Deserialize<List<Article>>(sanitizedJson, options);

                if (articles == null || articles.Count == 0)
                {
                    return BadRequest("No articles found in the provided JSON.");
                }

                foreach (Article article in articles)
                {
                    DataAccess.Instance.Insert<Article>(_settings.ApiDatabase, article);
                }

                return Ok("Articles inserted successfully.");
            }
            catch (JsonException jsonEx)
            {
                return BadRequest($"JSON deserialization error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }


        [HttpPut("article")]
        public IActionResult UpdateArticles([FromQuery] string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return BadRequest("The 'json' query parameter is required.");
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                string sanitizedJson = json.Replace("'", "\"");

                List<Article> articles = JsonSerializer.Deserialize<List<Article>>(sanitizedJson, options);

                if (articles == null || articles.Count == 0)
                {
                    return BadRequest("No articles found in the provided JSON.");
                }

                foreach (Article article in articles)
                {
                    DataAccess.Instance.Update<Article>(_settings.ApiDatabase, article);
                }

                return Ok("Articles updated successfully.");
            }
            catch (JsonException jsonEx)
            {
                return BadRequest($"JSON deserialization error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }


        [HttpDelete("article")]
        public IActionResult DeleteArticles([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("A valid article ID must be provided.");
            }

            try
            {
                DataAccess.Instance.Delete<Article>(_settings.ApiDatabase, id);
                return Ok($"Article with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the article: {ex.Message}");
            }
        }
    }
}
