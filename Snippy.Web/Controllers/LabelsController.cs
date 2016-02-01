using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Snippy.Data.UnitOfWork;
using Snippy.Web.Models.ViewModels;

namespace Snippy.Web.Controllers
{
    public class LabelsController : BaseController
    {
        public LabelsController(ISnippyData data) : base(data)
        {
        }

        public ActionResult AllSnippetsForLabel(int labelId)
        {
            var snippets = this.Data.Snippets
                .All()
                .Where(s => s.Labels.Select(l => l.Id).ToList().Contains(labelId))
                .ProjectTo<SnippetViewModel>();

            var label = this.Data.Labels.All()
                .FirstOrDefault(l => l.Id == labelId);

            ViewBag.labelName = label.Text;
            return this.View(snippets);
        }
    }
}