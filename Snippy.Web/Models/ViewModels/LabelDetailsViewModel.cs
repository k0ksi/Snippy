using System.Linq;
using AutoMapper;
using Snippy.Common;
using Snippy.Common.Mappings;
using Snippy.Models;

namespace Snippy.Web.Models.ViewModels
{
    public class LabelDetailsViewModel : IMapFrom<Label>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int SnippetCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Label, LabelDetailsViewModel>()
                .ForMember(x => x.SnippetCount,
                    cnf => cnf.MapFrom(m => m.Snippets.Any() ? m.Snippets.Count : GlobalConstants.DefaultSnippetCount));
        }
    }
}