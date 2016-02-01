using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using Snippy.Models;

namespace Snippy.Data
{
    public class SnippyDbContext : IdentityDbContext<User>, ISnippyDbContext
    {
        public SnippyDbContext()
            : base("SnippyConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Snippet> Snippets { get; set; }

        public IDbSet<Language> Languages { get; set; }

        public IDbSet<Label> Labels { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public static SnippyDbContext Create()
        {
            return new SnippyDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions
                .Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Snippet>()
                .HasMany(s => s.Labels)
                .WithMany(s => s.Snippets)
                .Map(m =>
                {
                    m.MapLeftKey("SnippetId");
                    m.MapRightKey("LabelId");
                    m.ToTable("SnippetLabels");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}