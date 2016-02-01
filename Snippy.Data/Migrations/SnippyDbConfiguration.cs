using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Snippy.Models;

namespace Snippy.Data.Migrations
{
    public sealed class SnippyDbConfiguration : DbMigrationsConfiguration<SnippyDbContext>
    {
        public SnippyDbConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Snippy.Data.SnippyDbContext";
        }

        protected override void Seed(SnippyDbContext context)
        {
            if (!context.Snippets.Any())
            {
                var users = this.LoadUsers(context);
                var labels = this.LoadLabels(context);
                var languages = this.LoadLanguages(context);
                var snippets = this.LoadSnippets(context, languages, labels);
                var comments = this.LoadComments(context);
            }
        }

        private List<Comment> LoadComments(SnippyDbContext context)
        {
            var admin = context.Users
                .FirstOrDefault(u => u.UserName == "admin");
            var someUser = context.Users
                .FirstOrDefault(u => u.UserName == "someUser");
            var newUser = context.Users
                .FirstOrDefault(u => u.UserName == "newUser");

            var ternaryOperatorMadnessSnippet = context.Snippets
                .FirstOrDefault(s => s.Title.Contains("Ternary Operator Madness"));

            var reverseStringSnippet = context.Snippets
                .FirstOrDefault(s => s.Title.Contains("Reverse a String"));

            var pointsArroundSnippet = context.Snippets
               .FirstOrDefault(s => s.Title.Contains("Points Around A Circle For GameObject Placement"));

            var numbersInputSnippet = context.Snippets
               .FirstOrDefault(s => s.Title.Contains("Numbers only in an input field"));

            var comments = new List<Comment>()
            {
                new Comment()
                {
                    Content = "Now that's really funny! I like it!",
                    UserId = admin.Id,
                    CreationTime = DateTime.Parse("30.10.2015 11:50:38"),
                    SnippetId = ternaryOperatorMadnessSnippet.Id
                },
                new Comment()
                {
                    Content = "Here, have my comment!",
                    UserId = newUser.Id,
                    CreationTime = DateTime.Parse("01.11.2015 15:52:42"),
                    SnippetId = ternaryOperatorMadnessSnippet.Id
                },
                new Comment()
                {
                    Content = "I didn't manage to comment first :(",
                    UserId = someUser.Id,
                    CreationTime = DateTime.Parse("02.11.2015 05:30:20"),
                    SnippetId = ternaryOperatorMadnessSnippet.Id
                },
                new Comment()
                {
                    Content = "That's why I love Python - everything is so simple!",
                    UserId = newUser.Id,
                    CreationTime = DateTime.Parse("27.10.2015 15:28:14"),
                    SnippetId = reverseStringSnippet.Id
                },
                new Comment()
                {
                    Content = "I have always had problems with Geometry in school. Thanks to you I can now do this without ever having to learn this damn subject",
                    UserId = someUser.Id,
                    CreationTime = DateTime.Parse("29.10.2015 15:08:42"),
                    SnippetId = pointsArroundSnippet.Id
                },
                new Comment()
                {
                    Content = "Thank you. However, I think there must be a simpler way to do this. I can't figure it out now, but I'll post it when I'm ready.",
                    UserId = admin.Id,
                    CreationTime = DateTime.Parse("03.11.2015 12:56:20"),
                    SnippetId = numbersInputSnippet.Id
                }
            };

            foreach (var comment in comments)
            {
                context.Comments.Add(comment);
            }

            context.SaveChanges();

            return comments;
        }

        private List<Snippet> LoadSnippets(SnippyDbContext context, List<Language> languages, List<Label> labels)
        {
            var admin = context.Users
                .FirstOrDefault(u => u.UserName == "admin");
            var someUser = context.Users
                .FirstOrDefault(u => u.UserName == "someUser");
            var newUser = context.Users
                .FirstOrDefault(u => u.UserName == "newUser");

            var snippets = new List<Snippet>()
            {
                new Snippet()
                {
                    Title = "Ternary Operator Madness",
                    Description = "This is how you DO NOT user ternary operators in C#!",
                    Code = "bool X = Glob.UserIsAdmin ? true : false;",
                    UserId = admin.Id,
                    CreationTime = DateTime.Parse("26.10.2015 17:20:33"),
                    LanguageId = languages[0].Id,
                    Labels = new HashSet<Label>() { labels[1] }
                },
                new Snippet()
                {
                    Title = "Points Around A Circle For GameObject Placement",
                    Description = "Determine points around a circle which can be used to place elements around a central point",
                    Code = "private Vector3 DrawCircle(float centerX, float centerY, float radius, float totalPoints, float currentPoint)\n{\n  float ptRatio = currentPoint / totalPoints;\n   float pointX = centerX + (Mathf.Cos(ptRatio * 2 * Mathf.PI)) * radius;\n    float pointY = centerY + (Mathf.Sin(ptRatio * 2 * Mathf.PI)) * radius;\n    Vector3 panelCenter = new Vector3(pointX, pointY, 0.0f);\n  return panelCenter;\n}",
                    UserId = admin.Id,
                    CreationTime = DateTime.Parse("26.10.2015 20:15:30"),
                    LanguageId = languages[0].Id,
                    Labels = new HashSet<Label>() { labels[6], labels[9] }
                },
                new Snippet()
                {
                    Title = "forEach. How to break?",
                    Description = "Array.prototype.forEach You can't break forEach. So use \"some\" or \"every\". Array.prototype.some some is pretty much the same as forEach but it break when the callback returns true. Array.prototype.every every is almost identical to some except it's expecting false to break the loop.",
                    Code = "var ary = [\"JavaScript\", \"Java\", \"CoffeeScript\", \"TypeScript\"];\nary.some(function (value, index, _ary) {\n 	console.log(index + \": \" + value);\n	return value === \"CoffeeScript\";\n});\n// output: \n// 0: JavaScript\n// 1: Java\n// 2: CoffeeScript\n \nary.every(function(value, index, _ary) {\n	console.log(index + \": \" + value);\n	return value.indexOf(\"Script\") > -1;\n});\n// output:\n// 0: JavaScript\n// 1: Java",
                    UserId = newUser.Id,
                    CreationTime = DateTime.Parse("27.10.2015 10:53:20"),
                    LanguageId = languages[1].Id,
                    Labels = new HashSet<Label>() { labels[2], labels[4], labels[5], labels[8] }
                },
                new Snippet()
                {
                    Title = "Numbers only in an input field",
                    Description = "Method allowing only numbers (positive / negative / with commas or decimal points) in a field",
                    Code = "$(\"#input\").keypress(function(event){\n	var charCode = (event.which) ? event.which : window.event.keyCode;\n	if (charCode <= 13) { return true; } \n	else {\n		var keyChar = String.\nfromCharCode(charCode);\n		var regex = new RegExp(\"[0-9,.-]\");\n		return regex.test(keyChar); \n	} \n});",
                    UserId = someUser.Id,
                    CreationTime = DateTime.Parse("28.10.2015 09:00:56"),
                    LanguageId = languages[1].Id,
                    Labels = new HashSet<Label>() { labels[5], labels[8] }
                },
                new Snippet()
                {
                    Title = "Create a link directly in an SQL query",
                    Description = "That's how you create links - directly in the SQL!",
                    Code = "SELECT DISTINCT\n              b.Id,\n              concat('<button type=\"\"button\"\" onclick=\"\"DeleteContact(', cast(b.Id as char), ')\"\">Delete...</button>') as lnkDelete\nFROM tblContact   b\nWHERE ....",
                    UserId = admin.Id,
                    CreationTime = DateTime.Parse("30.10.2015 11:20:00"),
                    LanguageId = languages[4].Id,
                    Labels = new HashSet<Label>() { labels[0], labels[1], labels[3] }
                },
                new Snippet()
                {
                    Title = "Reverse a String",
                    Description = "Almost not worth having a function for...",
                    Code = "def reverseString(s):\n	\"\"\"Reverses a string given to it.\"\"\"\n	return s[::-1]",
                    UserId = admin.Id,
                    CreationTime = DateTime.Parse("26.10.2015 09:35:13"),
                    LanguageId = languages[2].Id,
                    Labels = new HashSet<Label>() { labels[4] }
                },
                new Snippet()
                {
                    Title = "Pure CSS Text Gradients",
                    Description = "This code describes how to create text gradients using only pure CSS",
                    Code = "/* CSS text gradients */\nh2[data-text] {\n	position: relative;\n}\nh2[data-text]::after {\n	content: attr(data-text);\n	z-index: 10;\n	color: #e3e3e3;\n	position: absolute;\n	top: 0;\n	left: 0;\n	-webkit-mask-image: -webkit-gradient(linear, left top, left bottom, from(rgba(0,0,0,0)), color-stop(50%, rgba(0,0,0,1)), to(rgba(0,0,0,0)));\n",
                    UserId = someUser.Id,
                    CreationTime = DateTime.Parse("22.10.2015 19:26:42"),
                    LanguageId = languages[3].Id,
                    Labels = new HashSet<Label>() { labels[5], labels[8] }
                },
                new Snippet()
                {
                    Title = "Check for a Boolean value in JS",
                    Description = "How to check a Boolean value - the wrong but funny way :D",
                    Code = "var b = true;\n\nif (b.toString().length < 5) {\n  //...\n}",
                    UserId = newUser.Id,
                    CreationTime = DateTime.Parse("22.10.2015 05:30:04"),
                    LanguageId = languages[1].Id,
                    Labels = new HashSet<Label>() { labels[1] }
                },
            };

            foreach (var snippet in snippets)
            {
                context.Snippets.Add(snippet);
            }

            context.SaveChanges();

            return snippets;
        }

        private List<Language> LoadLanguages(SnippyDbContext context)
        {
            List<Language> languages = null;
            
            languages = new List<Language>()
            {
                new Language() { Name = "C#"},
                new Language() { Name = "JavaScript"},
                new Language() { Name = "Python"},
                new Language() { Name = "CSS"},
                new Language() { Name = "SQL"},
                new Language() { Name = "PHP"}
            };

            foreach (var language in languages)
            {
                context.Languages.Add(language);
            }

            context.SaveChanges();
            

            return languages;
        }

        private List<Label> LoadLabels(SnippyDbContext context)
        {
            List<Label> labels = null;
            
            labels = new List<Label>()
            {
                new Label() { Text = "bug"},
                new Label() { Text = "funny"},
                new Label() { Text = "jquery"},
                new Label() { Text = "mysql"},
                new Label() { Text = "useful"},
                new Label() { Text = "web"},
                new Label() { Text = "geometry"},
                new Label() { Text = "back-end"},
                new Label() { Text = "front-end"},
                new Label() { Text = "games"},
            };

            foreach (var label in labels)
            {
                context.Labels.Add(label);
            }

            context.SaveChanges();

            return labels;
        }

        private List<User> LoadUsers(SnippyDbContext context)
        {
            var users = new List<User>();
            
            var storeAdmin = new RoleStore<IdentityRole>(context);
            var storeManager = new RoleManager<IdentityRole>(storeAdmin);
            var role = new IdentityRole() { Name = "Admin" };

            storeManager.Create(role);
                        
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            var user = new User()
            {
                UserName = "admin",
                Email = "admin@snippy.softuni.com"
            };

            manager.Create(user, "adminPass123");
            manager.AddToRole(user.Id, "Admin");

            var someUser = new User()
            {
                UserName = "someUser",
                Email = "someUser@example.com"
            };

            var newUser = new User()
            {
                UserName = "newUser",
                Email = "new_user@gmail.com"
            };

            manager.Create(someUser, "someUserPass123");
            manager.Create(newUser, "userPass123");

            users.Add(user);
            users.Add(someUser);
            users.Add(newUser);
            

            return users;
        }
    }
}
