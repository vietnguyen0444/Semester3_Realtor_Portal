﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

#nullable disable

namespace NETAPI_SEM3.Models
{
    public partial class ProjectSem3DBContext : IdentityDbContext<IdentityUser>
    {
        public ProjectSem3DBContext()
        {
        }

        public ProjectSem3DBContext(DbContextOptions<ProjectSem3DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AboutU> AboutUs { get; set; }
        public virtual DbSet<AdPackage> AdPackages { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Faq> Faqs { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Mailbox> Mailboxes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberPackageDetail> MemberPackageDetails { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsCategory> NewsCategories { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AboutU>(entity =>
            {
                entity.HasKey(e => e.AboutId);

                entity.Property(e => e.AboutId).HasColumnName("About_Id");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Question).HasColumnType("text");

                entity.Property(e => e.Title).HasColumnType("text");
            });

            modelBuilder.Entity<AdPackage>(entity =>
            {
                entity.HasKey(e => e.PackageId);

                entity.ToTable("Ad_Package");

                entity.Property(e => e.PackageId).HasColumnName("Package_Id");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.NameAdPackage)
                    .HasColumnType("text")
                    .HasColumnName("Name_AdPackage");

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.IsShow).HasColumnName("isShow");

                entity.Property(e => e.Name).HasColumnType("text");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityId).HasColumnName("City_Id");

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.Name).HasColumnType("text");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_City_Country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.Name).HasColumnType("text");

                entity.Property(e => e.RegionId).HasColumnName("Region_Id");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Country_Region");
            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("FAQ");

                entity.Property(e => e.FaqId).HasColumnName("FAQ_Id");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.ImageId).HasColumnName("Image_Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NewsId).HasColumnName("News_Id");

                entity.Property(e => e.PropertyId).HasColumnName("Property_Id");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.NewsId)
                    .HasConstraintName("FK_Image_News");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK_Image_Property");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_Id");

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.MemberId).HasColumnName("Member_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.PackageId).HasColumnName("Package_Id");

                entity.Property(e => e.PaymentCard)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Payment_Card");

                entity.Property(e => e.PaymentCode)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Payment_Code");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Payment_Method");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Member");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Ad_Package");
            });

            modelBuilder.Entity<Mailbox>(entity =>
            {
                entity.HasKey(e => e.MailId);

                entity.ToTable("Mailbox");

                entity.Property(e => e.MailId).HasColumnName("Mail_Id");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyId).HasColumnName("Property_Id");

                entity.Property(e => e.Time).HasColumnType("date");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Mailboxes)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK_Mailbox_Property");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId).HasColumnName("Member_Id");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsShowMail).HasColumnName("isShowMail");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("Role_Id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MemberPackageDetail>(entity =>
            {
                entity.HasKey(e => e.MPDetailId);

                entity.ToTable("Member_Package_Detail");

                entity.Property(e => e.MPDetailId).HasColumnName("M_P_Detail_Id");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("Expiry_Date");

                entity.Property(e => e.MemberId).HasColumnName("Member_Id");

                entity.Property(e => e.PackageId).HasColumnName("Package_Id");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberPackageDetails)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Package_Detail_Member");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.MemberPackageDetails)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Package_Detail_Ad_Package");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.NewsId).HasColumnName("News_Id");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_News_News_Category");
            });

            modelBuilder.Entity<NewsCategory>(entity =>
            {
                entity.ToTable("News_Category");

                entity.Property(e => e.NewsCategoryId).HasColumnName("News_Category_Id");

                entity.Property(e => e.IsShow).HasColumnName("isShow");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Property");

                entity.Property(e => e.PropertyId).HasColumnName("Property_Id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BedNumber).HasColumnName("Bed_Number");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.CityId).HasColumnName("City_Id");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.GoogleMap).HasColumnType("text");

                entity.Property(e => e.MemberId).HasColumnName("Member_Id");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RoomNumber).HasColumnName("Room_Number");

                entity.Property(e => e.SoldDate)
                    .HasColumnType("date")
                    .HasColumnName("Sold_Date");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UploadDate)
                    .HasColumnType("date")
                    .HasColumnName("Upload_Date");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_Category");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_City");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Property_Member");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_Status");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.RegionId).HasColumnName("Region_Id");

                entity.Property(e => e.Name).HasColumnType("text");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");

                entity.Property(e => e.SettingId).HasColumnName("Setting_Id");

                entity.Property(e => e.AboutUsTitle)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Reviews).HasColumnType("text");

                entity.Property(e => e.Services)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ThumbnailAboutUs).HasColumnType("text");

                entity.Property(e => e.ThumbnailHome).HasColumnType("text");

                entity.Property(e => e.ThumbnailWebsite).HasColumnType("text");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
