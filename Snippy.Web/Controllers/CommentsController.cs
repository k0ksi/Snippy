using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Snippy.Data.UnitOfWork;
using Snippy.Models;
using Snippy.Web.Models.BindingModels;
using Snippy.Web.Models.ViewModels;

namespace Snippy.Web.Controllers
{
    [Authorize]
    public class CommentsController : BaseController
    {
        public CommentsController(ISnippyData data) : base(data)
        {
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CommentBindingModel model)
        {
            string urlAbsolute = this.Request.UrlReferrer.AbsolutePath;
            var snippetId = int.Parse(Regex.Replace(urlAbsolute, @"[^\d+]", ""));

            if (model != null && this.ModelState.IsValid)
            {
                model.UserId = this.UserProfile.Id;
                model.CreationTime = DateTime.Now;
                model.SnippetId = snippetId;
                var comment = Mapper.Map<Comment>(model);
                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var commentFromDb = this.Data.Comments
                    .All()
                    .Where(c => c.Id == comment.Id)
                    .ProjectTo<CommentDetailsViewModel>()
                    .FirstOrDefault();

                return this.PartialView("DisplayTemplates/CommentDetailsViewModel", commentFromDb);
            }

            return new EmptyResult();
        }

        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = this.Data.Comments
                .All()
                .FirstOrDefault(c => c.Id == id);

            var model = Mapper.Map<CommentViewModel>(comment);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int id)
        {
            var comment = this.Data.Comments
                .All()
                .FirstOrDefault(c => c.Id == id);
            var snippetId = 0;

            if (comment != null && comment.UserId == this.User.Identity.GetUserId())
            {
                snippetId = comment.SnippetId;
                this.Data.Comments.Delete(comment);
                this.Data.SaveChanges();

                return this.RedirectToAction("Details", "Snippets", new { id = snippetId });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Can not delete comment");
        }
    }
}