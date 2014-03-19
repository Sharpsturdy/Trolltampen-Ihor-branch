using System;
using System.Collections.Generic;
using System.Data.Entity;
using Trolltampen.Models;

namespace Trolltampen.DAL
{
    public class TTDBContext:System.Data.Entity.DbContext
    {
        public TTDBContext()
            : base("TrolltampenDev")
        {

        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<OAuthMembership> OAuthMemberships { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<VideoLink> VideoLinks { get; set; }
        public DbSet<PhotoGallery> PhotoGalleries { get; set; }
        public DbSet<GalleryPhoto> GalleryPhotos { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membership>()
                .HasMany<Role>(r => r.Roles)
                .WithMany(u => u.Members)
                .Map(m =>
                {
                    m.ToTable("webpages_UsersInRoles");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });
        }
    }
}