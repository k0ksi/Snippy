using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snippy.Models
{
    public class Label
    {
        private ICollection<Snippet> snippets;

        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public Label()
        {
            this.snippets = new HashSet<Snippet>();
        }

        public virtual ICollection<Snippet> Snippets
        {
            get { return this.snippets; }
            set { this.snippets = value; }
        }
    }
}