using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Json;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.OMDbAPI;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto.OMDbAPIDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.Controllers
{
    public class OmdbapiController : Controller
    {
        private readonly IOMDbAPIServices _omdbapiServices;
        private readonly FilminurkTARpe24Context _context;
        public OmdbapiController(IOMDbAPIServices omdbapiServices, FilminurkTARpe24Context context)
        {
            _omdbapiServices = omdbapiServices;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FindMovie(OMDbAPIsearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Import), new { title = model.Title });
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Import(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest();
            }

            var movieDto = await _omdbapiServices.OMDbSearchResult(title);
            if (movieDto != null)
            {
                var json = JsonSerializer.Serialize(movieDto, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                TempData["OMDbMovie"] = json;
                return RedirectToAction("CreateUpdate", "Movies");
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(OMDbAPIviewModel vm)
        {
            if (!ModelState.IsValid || vm == null)
            {
                return BadRequest();
            }
            var movie = new Movie
            {
                ID = Guid.NewGuid(),
                Title = vm.Title ?? string.Empty,
                Description = vm.Plot ?? "Imported from OMDb",
                Director = vm.Director ?? string.Empty,

                BuyPrice = "0",
                CurrentRating = 0,
                UserRating = 0,
            };
            if (!string.IsNullOrWhiteSpace(vm.Actors))
            {
                movie.Actors = vm.Actors.Split(',').Select(a => a.Trim()).ToList();
            }

            if (!string.IsNullOrWhiteSpace(vm.Runtime))
            {
                var m = Regex.Match(vm.Runtime, @"\d+");
                if (m.Success && int.TryParse(m.Value, out var minutes))
                {
                    movie.MovieLength = minutes;
                }
                else
                {
                    movie.MovieLength = 0;
                }
            }
            else
            {
                movie.MovieLength = 0;
            }

            DateOnly firstPublished;
            if (!string.IsNullOrWhiteSpace(vm.Released) && DateTime.TryParse(vm.Released, out var dt))
            {
                firstPublished = DateOnly.FromDateTime(dt);
            }
            else if (!string.IsNullOrWhiteSpace(vm.Year) && int.TryParse(vm.Year.Split('–').FirstOrDefault(), out var y))
            {
                firstPublished = new DateOnly(y, 1, 1);
            }
            else
            {
                firstPublished = DateOnly.FromDateTime(DateTime.Now);
            }
            movie.FirstPublished = firstPublished;

            await _context.Movies.AddAsync(movie).ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(vm.Poster))
            {
                var poster = new FileToAPI
                {
                    ImageID = Guid.NewGuid(),
                    ExistingFilePath = vm.Poster,
                    MovieID = movie.ID,
                    IsPoster = true
                };
                await _context.FilesToAPI.AddAsync(poster).ConfigureAwait(false);
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return RedirectToAction("Index", "Movies");
        }

    }
}