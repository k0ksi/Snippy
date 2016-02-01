using System;
using Snippy.Common.Mappings;
using Snippy.Models;

namespace Snippy.Web.Models.ViewModels
{
    public class CommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

        public string UserUserName { get; set; }

        public string SnippetTitle { get; set; }

        public int SnippetId { get; set; }
    }
}