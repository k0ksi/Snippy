using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Snippy.Data.UnitOfWork;
using Snippy.Web.Models.ViewModels;

namespace Snippy.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ISnippyData data) : base(data)
        {
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var snippets = this.Data.Snippets
                .All()
                .OrderByDescending(s => s.CreationTime)
                .Take(5)
                .ProjectTo<SnippetViewModel>();

            var comments = this.Data.Comments
                .All()
                .OrderByDescending(c => c.CreationTime)
                .Take(5)
                .ProjectTo<CommentViewModel>();

            var labels = this.Data.Labels
                .All()
                .OrderByDescending(l => l.Snippets.Count)
                .Take(5)
                .ProjectTo<LabelDetailsViewModel>();

            var homePageViewModel = HomePageViewModel.Create(snippets, comments, labels);

            return View(homePageViewModel);
        }
    }
}