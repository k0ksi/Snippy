using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Snippy.Data.UnitOfWork;
using Snippy.Web.Models.ViewModels;

namespace Snippy.Web.Controllers
{
    public class LanguagesController : BaseController
    {
        public LanguagesController(ISnippyData data) : base(data)
        {
        }

        public ActionResult AllSnippetsForLanguage(int id)
        {
            var snippets = this.Data.Snippets
                .All()
                .Where(s => s.Language.Id == id)
                .ProjectTo<SnippetViewModel>();

            var language = this.Data.Languages.Find(id);
            ViewBag.languageName = language.Name;

            return this.View(snippets);
        }
    }
}