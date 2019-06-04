using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Login.Database;
using Login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Login.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class MovieController : ControllerBase
    {

        ILogger<MovieController> _logger;
        private readonly DatabaseContext context;

        public MovieController(ILogger<MovieController> logger, DatabaseContext context)
        {
            this.context = context;
            _logger = logger;
        }

        //Get Movie From Database (All User)
        [HttpGet("All")]
        [Authorize(Roles = "User,Administrator")]
        public IActionResult GetMovie()
        {
            try
            {
                IEnumerable result = context.Movie.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        //Add Move in Database (Administrator)
        [HttpPost("Add")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            try
            {
                context.Movie.Add(movie);
                context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        
        [HttpGet("Show")]
        [Authorize(Roles = "User,Administrator,Visitor")]
        public IActionResult ShowMovie()
        {
            try
            {
                List<Movie> result = context.Movie.ToList();
                List<MovieShow> Show = result.Select(s =>
                  new MovieShow
                  {
                      Title = s.Title,
                      Url = s.Url
                  }).ToList();
                return Ok(Show);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

    }
}