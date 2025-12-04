using Filminurk.ApplicationServices.Services;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Data;
using Filminurk.Models.FavouriteLists;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavouriteListsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly FavouriteListsServices _favouriteListsServices;

        public FavouriteListsController(FilminurkTARpe24Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var resultingLists = _context.FavouriteLists
                .OrderByDescending(y => y.ListCreatedAt)
                .Select(x => new FavouriteListsIndexViewModel
                {
                    FavouriteListID = x.FavouriteListID,
                    ListBelongsToUser = x.ListBelongsToUser,
                    IsMovieOrActor = x.IsMovieOrActor,
                    ListName = x.ListName,
                    ListDescription = x.ListDescription,
                    ListCreatedAt = x.ListCreatedAt,
                    ListDeletedAt = (DateTime)x.ListDeletedAt,
                    Image = (List<FavouriteListsIndexImageViewModel>)_context.FilesToDatabase
                    .Where(ml => ml.ListID == x.FavouriteListID)
                    .Select(li => new FavouriteListsIndexImageViewModel()
                    {
                        ListID = (Guid)li.ListID,
                        ImageID = li.ImageID,
                        ImageData = li.ImageData,
                        ImageTitle = li.ImageTitle,
                        Image = string.Format("data:image/gif;base64, {0}", Convert.ToBase64String(li.ImageData)),

                    })

                });
            return View(resultingLists);
        }

        [HttpPost]
        public async Task<IActionResult> UserTogglePrivacy(Guid id)
        {
            FavouriteList thisList = await _favouriteListsServices.DetailsAsync(id);

            FavouriteListDTO updatedList = new FavouriteListDTO();
            updatedList.FavouriteListID = thisList.FavouriteListID;
            updatedList.ListBelongsToUser = thisList.ListBelongsToUser;
            updatedList.ListName = thisList.ListName;
            updatedList.ListDescription = thisList.ListDescription;
            updatedList.IsPrivate = thisList.IsPrivate;
            updatedList.ListOfMovies = thisList.ListOfMovies;
            updatedList.IsReported = thisList.IsReported;
            updatedList.IsMovieOrActor = thisList.IsMovieOrActor;
            updatedList.ListCreatedAt = thisList.ListCreatedAt;
            updatedList.ListModifiedAt = DateTime.Now;
            updatedList.ListDeletedAt = thisList.ListDeletedAt;
            ViewData["UpdateSericeType"] = "Private";

            var result = await _favouriteListsServices.Update(updatedList, "Private");
            if (result == null)
            {
                return NotFound();
            }
            //if (result.IsPrivate != !result.IsPrivate)
            //{
            //  return BadRequest();
            //}
            //return RedirectToAction("UserDetails", result.FavouriteListID);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> UserDelete(Guid id)
        {
            var deletedList = await _favouriteListsServices.DetailsAsync(id);
            deletedList.ListDeletedAt = DateTime.Now;

            var dto = new FavouriteListDTO();
            dto.FavouriteListID = deletedList.FavouriteListID;
            dto.ListBelongsToUser = deletedList.ListBelongsToUser;
            dto.ListName = deletedList.ListName;
            dto.ListDescription = deletedList.ListDescription;
            dto.IsPrivate = deletedList.IsPrivate;
            dto.ListOfMovies = deletedList.ListOfMovies;
            dto.IsReported = deletedList.IsReported;
            dto.IsMovieOrActor = deletedList.IsMovieOrActor;
            dto.ListCreatedAt = deletedList.ListCreatedAt;
            dto.ListModifiedAt = DateTime.Now;
            dto.ListDeletedAt = DateTime.Now;
            ViewData["UpdateSericeType"] = "Delete";

            var result = await _favouriteListsServices.Update(dto, "Delete");
            if (result == null)
            {
                NotFound();
            }
            return RedirectToAction("Index");

        }
    }
}
