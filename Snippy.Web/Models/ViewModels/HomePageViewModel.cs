using System.Collections.Generic;
using System.Linq;
using Snippy.Models;

namespace Snippy.Web.Models.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<SnippetViewModel> Snippets { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public IEnumerable<LabelDetailsViewModel> Labels { get; set; }

        public static HomePageViewModel Create(IQueryable<SnippetViewModel> snippets, IQueryable<CommentViewModel> comments, IQueryable<LabelDetailsViewModel> labels)
        {
            return new HomePageViewModel()
            {
                Snippets = snippets,
                Comments = comments,
                Labels = labels
            };
        }
    }
}