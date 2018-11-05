using System;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer
{

    public class SOVAContext : DbContext
    {
        public DbSet<Annotations> Annotations { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Mark> Marked { get; set; }
        //public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SearchHistories> SearchHistory{ get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("host=localhost;db=stackoverflow;uid=postgres;pwd=521313");
           


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Map Class property: Post. Principal entity. Still has relation to comment
            modelBuilder.Entity<Post>().ToTable("posts");
            modelBuilder.Entity<Post>().HasKey(x => x.Id);
            modelBuilder.Entity<Post>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Post>().Property(x => x.PostType).HasColumnName("posttype");
            //modelBuilder.Entity<Post>().Property(x => x.ParentId).HasColumnName("parentid");
            modelBuilder.Entity<Post>().Property(x => x.AcceptedAnswerId).HasColumnName("acceptedanswerid");
            modelBuilder.Entity<Post>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Entity<Post>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<Post>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Post>().Property(x => x.ClosedDate).HasColumnName("closeddate");
            modelBuilder.Entity<Post>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<Post>().Property(x => x.AuthorId).HasColumnName("authorid");
            modelBuilder.Entity<Post>().HasDiscriminator(x => x.PostType)
                .HasValue<Question>(1)
                .HasValue<Answer>(2);

            //do we need to make this in database?
            modelBuilder.Entity<Question>().Property(x => x.Title).HasColumnName("title");
            

            modelBuilder.Entity<Answer>().Property(x => x.ParentId).HasColumnName("parentid");

            //Map Class Propert: Author
            modelBuilder.Entity<Author>().ToTable("authors");
            modelBuilder.Entity<Author>().Property(x => x.AuthorId).HasColumnName("id");
            modelBuilder.Entity<Author>().Property(x => x.DisplayName).HasColumnName("displayname");
            modelBuilder.Entity<Author>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<Author>().Property(x => x.Location).HasColumnName("location");
            modelBuilder.Entity<Author>().Property(x => x.Age).HasColumnName("age");

            //Map Class Propert: Annotations
            modelBuilder.Entity<Annotations>().ToTable("annotations");
            modelBuilder.Entity<Annotations>().HasKey(x => new {x.UserId, x.PostId});
            modelBuilder.Entity<Annotations>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Annotations>().Property(x => x.PostId).HasColumnName("postid");
            modelBuilder.Entity<Annotations>().Property(x => x.UserId).HasColumnName("userid");

            //Map Class Propert: Mark
            modelBuilder.Entity<Mark>().ToTable("marked");
            modelBuilder.Entity<Mark>().HasKey(x => new { x.UserId, x.PostId });
            modelBuilder.Entity<Mark>().Property(x => x.PostId).HasColumnName("postid");
            modelBuilder.Entity<Mark>().Property(x => x.UserId).HasColumnName("userid");


            //Map Class Propert: Comment 
            modelBuilder.Entity<Comment>().ToTable("comments");
            modelBuilder.Entity<Comment>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Comment>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Entity<Comment>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<Comment>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Comment>().Property(x => x.PostId).HasColumnName("parent");
            modelBuilder.Entity<Comment>().Property(x => x.AuthorId).HasColumnName("authorid");
            modelBuilder.Entity<Comment>()
                .HasOne<Question>(c => c.Question)
                .WithMany(q => q.Comments)
                .HasForeignKey(c => c.PostId);
            modelBuilder.Entity<Comment>()
                .HasOne<Answer>(c => c.Answer)
                .WithMany(q => q.Comments)
                .HasForeignKey(c => c.PostId);




            //Map class property: PostTag Id is still foreign key. Need to ask about this as well. 
            //tag does not need to be foreign key to tag. Can just use distinct keyword
            modelBuilder.Entity<PostTag>().ToTable("posttags");
            modelBuilder.Entity<PostTag>().Property(x => x.PostTagId).HasColumnName("id");
            modelBuilder.Entity<PostTag>().Property(x => x.Tag).HasColumnName("tag");



            //Map class property for users

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");


            //Map SearchHistory Should have composite primary key of search userid and date
            modelBuilder.Entity<SearchHistories>().ToTable("searchhistory");
            modelBuilder.Entity<SearchHistories>().HasKey(x => new { x.Search, x.UserId, x.Date });
            modelBuilder.Entity<SearchHistories>().Property(x => x.Search).HasColumnName("search");
            modelBuilder.Entity<SearchHistories>().Property(x => x.UserId).HasColumnName("userid");
            modelBuilder.Entity<SearchHistories>().Property(x => x.Date).HasColumnName("date");

        }

    }
    

}
