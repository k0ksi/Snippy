using System;
using System.ComponentModel.DataAnnotations;
using Snippy.Common;
using Snippy.Models;

namespace Snippy.Web.Models.BindingModels
{
    public class SnippetBindingModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.SnippetCodeValidationMessage)]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.SnippetCodeValidationMessage)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.SnippetCodeValidationMessage)]
        public string Description { get; set; }

        public string UserId { get; set; }

        public DateTime CreationTime { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Display(Name = "Labels")]
        public string Labels { get; set; }

        public static SnippetBindingModel CreateFromSnippet(Snippet e)
        {
            return new SnippetBindingModel()
            {
                Title = e.Title,
                CreationTime = e.CreationTime,
                Description = e.Description,
                Code = e.Code,
                LanguageId = e.LanguageId
            };
        }
    }
}