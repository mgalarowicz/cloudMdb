using System;
using CloudMovieDatabase.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudMovieDatabase.API.Validation
{
    public class MovieValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context) 
        {
            foreach (var item in context.ActionArguments)
            {
                var movie = item.Value as Movie;
                if (movie == null)
                {
                    continue;
                }

                if (movie.StarringActors.Count < 1 || movie.StarringActors == null)
                {
                    context.ModelState.AddModelError("MovieValidationError",
                        "Movie has to have at least one actor");
                }

                if (DateTime.Now.Year < movie.Year)
                {
                    context.ModelState.AddModelError("MovieValidationError",
                        $"You cannot input year that is grater than our actual year: {DateTime.Now.Year}.");
                }

                break;
            }

             if (!context.ModelState.IsValid)
             {
                context.Result = new BadRequestObjectResult(context.ModelState);
             }   
        }
    }
}