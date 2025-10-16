using Microsoft.AspNetCore.Mvc;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        public MoviesController (FilminurkTARpe24Context context, IMovieServices movieServices)
        {
            _context = context;
            _movieServices = movieServices;
        }
        public IActionResult Index()
        {
            var result = _context.Movies.Select(x => new MoviesIndexViewModel
            {
                ID = x.ID,
                Title = x.Title,
                FirstPublished = x.FirstPublished,
                CurrentRating = x.CurrentRating,
                UserRating = x.UserRating,
                MovieLength = x.MovieLength,
            });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            MoviesCreateViewModel result = new();
            return View("Create", result);
        }
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(MoviesCreateViewModel vm)
        {
            var dto = new MoviesDTO()
            {
                ID = vm.ID,
                Title = vm.Title,
                Description = vm.Description,
                FirstPublished = vm.FirstPublished,
                Director = vm.Director,
                Actor = vm.Actor,
                CurrentRating = vm.CurrentRating,
                UserRating = vm.UserRating,
                BuyPrice = vm.BuyPrice,
                MovieLength = vm.MovieLength,
                EntryCreatedAt = vm.EntryCreatedAt,
                EntryModifiedAt = vm.EntryModifiedAt,
            };
            var result = await _movieServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);
            if(movie == null)
            {
                return NotFound();
            }
            var vm = new MoviesDeleteViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.CurrentRating = movie.CurrentRating;
            vm.UserRating = movie.UserRating;
            vm.MovieLength = movie.MovieLength;
            vm.BuyPrice = movie.BuyPrice;
            vm.Actor = movie.Actor;
            vm.Director = movie.Director;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var movie = await _movieServices.Delete(id);
            if (movie == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));        }

        
    }
}
