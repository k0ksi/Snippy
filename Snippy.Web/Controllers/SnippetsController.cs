using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PagedList;
using Snippy.Common;
using Snippy.Data.UnitOfWork;
using Snippy.Models;
using Snippy.Web.Extensions;
using Snippy.Web.Models.BindingModels;
using Snippy.Web.Models.ViewModels;
using System.Web.Mvc.Expressions;
using Microsoft.AspNet.Identity;

namespace Snippy.Web.Controllers
{
    public class SnippetsController : BaseController
    {
        public SnippetsController(ISnippyData data) : base(data)
        {
        }

        [Authorize]
        public ActionResult MySnippets()
        {
            var snippets = this.Data.Snippets
                .All()
                .Where(s => s.UserId == this.UserProfile.Id)
                .ProjectTo<SnippetViewModel>();

            return this.View(snippets);
        }
        
        [AllowAnonymous]
        public ActionResult AllSnippets(int? page)
        {
            var snippets = this.Data.Snippets
                .All()
                .OrderByDescending(s => s.CreationTime)
                .ProjectTo<SnippetViewModel>()
                .ToPagedList(page ?? GlobalConstants.DefaultStartPage, GlobalConstants.DefaultPageSize);

            return this.View(snippets);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var snippet = this.Data.Snippets
                .All()
                .Include(s => s.Comments)
                .Include(s => s.Labels)
                .FirstOrDefault(s => s.Id == id);

            var model = Mapper.Map<SnippetDetailsViewModel>(snippet);
            var comments = model.Comments
                .OrderByDescending(c => c.CreationTime);
            model.Comments = comments;

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            this.LoadLanguages();
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(SnippetBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var languagesAll = this.LoadLanguages();
                var snippet = new Snippet()
                {
                    Code = model.Code,
                    Title = model.Title,
                    Description = model.Description,
                    LanguageId = model.LanguageId,
                    Language = this.Data.Languages.Find(model.LanguageId),
                    UserId = this.User.Identity.GetUserId(),
                    CreationTime = DateTime.Now
                };
                this.Data.Snippets.Add(snippet);

                var regex = @"\s*;*([a-zA-Z\-]+);*\s*";
                var labels = Regex.Matches(model.Labels, regex);
                foreach (Match labelFromInput in labels)
                {
                    foreach (Capture text in labelFromInput.Captures)
                    {
                        var label = this.Data.Labels
                        .All()
                        .FirstOrDefault(l => l.Text == text.Value);
                        if (label != null)
                        {
                            snippet.Labels.Add(label);
                            this.Data.SaveChanges();
                        }
                        else
                        {
                            string labelValue = text.Value.Replace(";", "").Replace(" ", "");
                            var newLabel = new Label()
                            {
                                Text = labelValue
                            };
                            this.Data.Labels.Add(newLabel);
                            this.Data.SaveChanges();
                            var newlyCreatedLabel = this.Data.Labels
                            .All()
                            .FirstOrDefault(l => l.Text == text.Value);
                            snippet.Labels.Add(newlyCreatedLabel);
                            this.Data.SaveChanges();
                        }
                    }
                }

                this.Data.SaveChanges();
                this.AddNotification(GlobalConstants.SnippetCreate, NotificationType.SUCCESS);

                return this.RedirectToAction(x => x.Details(snippet.Id));
            }

            this.LoadLanguages();
            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadLanguages();
            var snippet = this.Data.Snippets
                .All()
                .FirstOrDefault(s => s.Id == id);
            if (snippet == null)
            {
                this.AddNotification(GlobalConstants.SnippetMissing, NotificationType.ERROR);
                return this.RedirectToAction("MySnippets");
            }

            var model = SnippetBindingModel.CreateFromSnippet(snippet);
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, SnippetBindingModel model)
        {
            var snippetToEdit = this.Data.Snippets.Find(id);
            if (snippetToEdit == null)
            {
                this.AddNotification(GlobalConstants.SnippetMissing, NotificationType.ERROR);
                return this.RedirectToAction("MySnippets");
            }

            if (model != null && this.ModelState.IsValid)
            {
                snippetToEdit.Code = model.Code;
                snippetToEdit.Title = model.Title;
                snippetToEdit.Description = model.Description;
                snippetToEdit.CreationTime = snippetToEdit.CreationTime;
                snippetToEdit.LanguageId = model.LanguageId;

                var regex = @"\s*;*([a-zA-Z\-]+);*\s*";
                var labels = Regex.Matches(model.Labels, regex);
                foreach (Match labelFromInput in labels)
                {
                    foreach (Capture text in labelFromInput.Captures)
                    {
                        var label = this.Data.Labels
                        .All()
                        .FirstOrDefault(l => l.Text == text.Value);
                        if (label != null)
                        {
                            snippetToEdit.Labels.Add(label);
                            this.Data.SaveChanges();
                        }
                        else
                        {
                            string labelValue = text.Value.Replace(";", "").Replace(" ", "");
                            var newLabel = new Label()
                            {
                                Text = labelValue
                            };
                            this.Data.Labels.Add(newLabel);
                            this.Data.SaveChanges();
                            var newlyCreatedLabel = this.Data.Labels
                            .All()
                            .FirstOrDefault(l => l.Text == text.Value);
                            snippetToEdit.Labels.Add(newlyCreatedLabel);
                            this.Data.SaveChanges();
                        }
                    }
                }

                this.Data.SaveChanges();
                this.AddNotification(GlobalConstants.SnippetEdited, NotificationType.SUCCESS);

                return this.RedirectToAction(x => x.Details(snippetToEdit.Id));
            }

            this.LoadLanguages();
            return this.View(model);
        }

        private IQueryable<SelectListItem> LoadLanguages()
        {
            var languages = this.Data.Languages
                .All()
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });
            this.ViewBag.Languages = languages;

            return languages;
        }
    }
}