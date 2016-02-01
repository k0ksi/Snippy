using System;
using Snippy.Common.Mappings;
using Snippy.Models;

namespace Snippy.Web.Models.ViewModels
{
    public class CommentDetailsViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

        public string UserUserName { get; set; }
    }
}