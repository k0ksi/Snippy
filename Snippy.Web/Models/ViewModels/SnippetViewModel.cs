using System.Collections.Generic;
using Snippy.Common.Mappings;
using Snippy.Models;

namespace Snippy.Web.Models.ViewModels
{
    public class SnippetViewModel : IMapFrom<Snippet>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<LabelViewModel> Labels { get; set; }
    }
}