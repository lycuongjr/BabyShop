namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BabyShopDbContext : DbContext
    {
        public BabyShopDbContext()
            : base("name=BabyShopDbContext")
        {
        }

        public virtual DbSet<Abount> Abounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<ContentTag> ContentTags { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Feddback> Feddbacks { get; set; }
        public virtual DbSet<Menuu> Menuus { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abount>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Abount>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<Category>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<CategoryProduct>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryProduct>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryProduct>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<Content>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<ContentTag>()
                .Property(e => e.TagID)
                .IsUnicode(false);

            modelBuilder.Entity<Footer>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.PromotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<Slide>()
                .Property(e => e.Create)
                .IsFixedLength();

            modelBuilder.Entity<Slide>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);
        }
    }
}
