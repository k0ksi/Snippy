using System;
using System.ComponentModel.DataAnnotations;
using Snippy.Common;
using Snippy.Common.Mappings;
using Snippy.Models;

namespace Snippy.Web.Models.BindingModels
{
    public class CommentBindingModel : IMapTo<Comment>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.CommentContentValidationMessage)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public DateTime CreationTime { get; set; }

        public int SnippetId { get; set; }
    }
}