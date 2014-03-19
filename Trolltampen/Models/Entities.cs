using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trolltampen.Models
{
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }

    [Table("webpages_Membership")]
    public class Membership
    {
        public Membership()
        {
            Roles = new List<Role>();
            OAuthMemberships = new List<OAuthMembership>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        [StringLength(128)]
        public string ConfirmationToken { get; set; }
        public bool? IsConfirmed { get; set; }
        public DateTime? LastPasswordFailureDate { get; set; }
        public int PasswordFailuresSinceLastSuccess { get; set; }
        [Required, StringLength(128)]
        public string Password { get; set; }
        public DateTime? PasswordChangedDate { get; set; }
        [Required, StringLength(128)]
        public string PasswordSalt { get; set; }
        [StringLength(128)]
        public string PasswordVerificationToken { get; set; }
        public DateTime? PasswordVerificationTokenExpirationDate { get; set; }

        public ICollection<Role> Roles { get; set; }

        [ForeignKey("UserId")]
        public ICollection<OAuthMembership> OAuthMemberships { get; set; }
    }

    [Table("webpages_OAuthMembership")]
    public class OAuthMembership
    {
        [Key, Column(Order = 0), StringLength(30)]
        public string Provider { get; set; }

        [Key, Column(Order = 1), StringLength(100)]
        public string ProviderUserId { get; set; }

        public int UserId { get; set; }

        [Column("UserId"), InverseProperty("OAuthMemberships")]
        public Membership User { get; set; }
    }

    [Table("webpages_Roles")]
    public class Role
    {
        public Role()
        {
            Members = new List<Membership>();
        }

        [Key]
        public int RoleId { get; set; }
        [StringLength(256)]
        public string RoleName { get; set; }

        public ICollection<Membership> Members { get; set; }
    }

    public class Category
    {
        public int ID { get; set; }
        [Display(Name = "Category name")]
        [Required(ErrorMessage = "Please input category name")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int OrderNum { get; set; }
        [Display(Name = "Media Type")]
        [ForeignKey("MediaType")]
        public int MediaTypeID { get; set; }
        public int ContentID { get; set; }


        public virtual ICollection<Article> Articles { get; set; }
        public virtual MediaType MediaType { get; set; }
    }
    public class Article
    {
        public int ID { get; set; }
        [Display(Name = "Headline")]
        [Required(ErrorMessage = "Please input headline")]
        public string Headline { get; set; }
        [Display(Name = "Ingress")]
        public string Ingress { get; set; }
        [Display(Name = "Text")]
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public int OrderNum { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        [Display(Name = "External Link")]
        public string ExtLink { get; set; }
        [ForeignKey("MediaType")]
        [Display(Name = "Media Type")]
        public int MediaTypeID { get; set; }
        public int ContentID { get; set; }

        public virtual Category Category { get; set; }
        public virtual MediaType MediaType { get; set; }

    }

    public class MediaType
    {
        public int ID { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<PhotoGallery> PhotoGalleries { get; set; }
        public virtual ICollection<VideoLink> VideoLinks { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }

    public class Photo
    {
        public int ID { get; set; }
        [ForeignKey("MediaType")]
        public int MediaTypeID { get; set; }
        public string FileName { get; set; }

        public virtual MediaType MediaType { get; set; }
    }

    public class VideoLink
    {
        public int ID { get; set; }
        [ForeignKey("MediaType")]
        public int MediaTypeID { get; set; }
        public string Link { get; set; }
        public string Hosting { get; set; }

        public virtual MediaType MediaType { get; set; }
    }

    public class PhotoGallery
    {
        public int ID { get; set; }
        [ForeignKey("MediaType")]
        public int MediaTypeID { get; set; }

        public virtual MediaType MediaType { get; set; }
        public virtual ICollection<GalleryPhoto> GalleryPhotos { get; set; }
    }

    public class GalleryPhoto
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        [ForeignKey("PhotoGallery")]
        public int PhotoGalleryID { get; set; }
        public int OrderNum { get; set; }
        public bool isActive { get; set; }

        public virtual PhotoGallery PhotoGallery { get; set; }
    }

    public class Contact
    {
        public int ID { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Please provide valid email")]
        [EmailAddress(ErrorMessage = "Please provide valid email")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please provide first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please provide last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please provide position")]
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Please provide valid phone number")]
        [Phone(ErrorMessage = "Please provide valid Phone number")]
        public string PhoneNum { get; set; }

        public string City { get; set; }

        public string Sted { get; set; }

        public string PostNo { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        [ForeignKey("MediaType")]
        public int MediaTypeID { get; set; }
        public int ContentID { get; set; }

        public int OrderNum { get; set; }
        public bool IsActive { get; set; }


        public virtual MediaType MediaType { get; set; }

    }

}