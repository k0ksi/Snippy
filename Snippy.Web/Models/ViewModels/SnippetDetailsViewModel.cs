using System;
using System.Collections.Generic;
using Snippy.Common.Mappings;
using Snippy.Models;

namespace Snippy.Web.Models.ViewModels
{
    public class SnippetDetailsViewModel : IMapFrom<Snippet>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string LanguageName { get; set; }

        public int LanguageId { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreationTime { get; set; }

        public IEnumerable<LabelViewModel> Labels { get; set; }

        public IEnumerable<CommentDetailsViewModel> Comments { get; set; }
    }
}