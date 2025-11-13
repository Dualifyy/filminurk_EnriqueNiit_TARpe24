using System.Reflection.Metadata.Ecma335;
using Filminurk.ApplicationServices.Services;
using Filminurk.Core;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.UserComments;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IUserCommentsServices _userCommentsServices;
        public UserCommentsController(FilminurkTARpe24Context context,
            IUserCommentsServices userCommentsServices)
        {
            _context = context;
            _userCommentsServices = userCommentsServices;
        }

        public IActionResult Index()
        {
            var result = _context.UserComments
                .Select(c => new UserCommentsIndexViewModel
                {
                    CommentID = c.CommentID,
                    CommentBody = c.CommentBody,

                    CommentCreatedAt = c.CommentCreatedAt,
                    CommentDeletedAt = c.CommentDeletedAt,


                });
            return View(result);
        }
        [HttpGet]
        public IActionResult NewComment()
        {
            //TODO: erista kas tegemist on admini, või tavakasutajaga
            UserCommentsCreateViewModel newcomment = new();
            return View(newcomment);
        }
        [HttpPost, ActionName("NewComment")]
        //meetodile ei tohi panna allowAnonymous
        public async Task<IActionResult> NewCommentPost(UserCommentsCreateViewModel newcommentVM)
        {
            if (ModelState.IsValid)
            {
                // check dto
                var dto = new UserCommentDTO() { };

                dto.CommentID = newcommentVM.CommentID;
                dto.CommentBody = newcommentVM.CommentBody;
                dto.CommenterUserID = newcommentVM.CommenterUserID;
                dto.CommentedScore = newcommentVM.CommentedScore;
                dto.CommentCreatedAt = newcommentVM.CommentCreatedAt;
                dto.CommentModifiedAt = newcommentVM.CommentModifiedAt;
                dto.IsHelpful = newcommentVM.IsHelpful;
                dto.IsHarmful = newcommentVM.IsHarmful;

                
                var result = await _userCommentsServices.NewComment(dto);
                if (result == null)
                {
                    return NotFound();
                }
                //TODO: erista ära kas tegu on admini või kasutajaga, admin tagastab admin-comments-index, kasutaja aga vastava filmi juurde
                return RedirectToAction("Index");
                //return RedirectToAction("Details", "Movies", id)
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> DetailsAdmin(Guid id)
        {
            var requestedComment = await _userCommentsServices.DetailAsync(id);

            if (requestedComment == null) { return NotFound(); }

            var commentVM = new UserCommentsIndexViewModel();

            commentVM.CommentID = requestedComment.CommentID;
            commentVM.CommentBody = requestedComment.CommentBody;
            commentVM.CommenterUserID = requestedComment.CommenterUserID;
            commentVM.CommentedScore = requestedComment.CommentedScore;
            commentVM.CommentCreatedAt = requestedComment.CommentCreatedAt;
            commentVM.CommentModifiedAt= requestedComment.CommentModifiedAt;
            commentVM.CommentDeletedAt = requestedComment.CommentDeletedAt;

            return View(commentVM);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            var deleteEntry = await _userCommentsServices.DetailAsync(id);

            if (deleteEntry == null)
            {
                return NotFound();
            }
            var commentVM = new UserCommentsIndexViewModel();


            commentVM.CommentID = deleteEntry.CommentID;
            commentVM.CommentBody = deleteEntry.CommentBody;
            commentVM.CommenterUserID = deleteEntry.CommenterUserID;
            commentVM.CommentedScore = deleteEntry.CommentedScore;
            commentVM.CommentCreatedAt = deleteEntry.CommentCreatedAt;
            commentVM.CommentModifiedAt = deleteEntry.CommentModifiedAt;
            commentVM.CommentDeletedAt = deleteEntry.CommentDeletedAt;

            return View("DeleteAdmin",commentVM);

        }
        [HttpPost, ActionName("DeleteCommentAdmin")]
        public async Task<IActionResult> DeleteAdminPost(Guid id)
        {
            var deleteThisComment = await _userCommentsServices.Delete(id);
            if (deleteThisComment == null) { return NotFound(); }
            return RedirectToAction("Index");
        }
    }
}
