using Snippy.Common.Mappings;
using Snippy.Models;

namespace Snippy.Web.Models.ViewModels
{
    public class LabelViewModel : IMapFrom<Label>
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}