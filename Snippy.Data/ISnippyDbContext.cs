using System.Data.Entity;
using Snippy.Models;

namespace Snippy.Data
{
    public interface ISnippyDbContext
    {
        IDbSet<Snippet> Snippets { get; set; }

        IDbSet<Language> Languages { get; set; }

        IDbSet<Label> Labels { get; set; }

        IDbSet<Comment> Comments { get; set; }

        int SaveChanges();
    }
}