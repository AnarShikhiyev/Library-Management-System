using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectLibrary_Back.Models;

public partial class LibraryProjectContext : DbContext
{
    public LibraryProjectContext()
    {
    }

    public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<Rating> Ratings { get; set; }
    public virtual DbSet<Remender> Remenders { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    

    public virtual DbSet<Review> Reviews{ get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HJS933K\\SQLEXPRESS01;Database=LibraryProject;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC0797E07FA2");

            entity.Property(e => e.AuthorBirthDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AuthorName).HasMaxLength(50);
            entity.Property(e => e.AuthorSurname).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC077261F067");

            entity.Property(e => e.BookPublicationYear).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.BookTitle).HasMaxLength(100);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Books__AuthorId__5165187F");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Books__CategoryI__52593CB8");

            entity.HasOne(d => d.Language).WithMany(p => p.Books)
                .HasForeignKey(d => d.LanguageId)
                .HasConstraintName("FK__Books__LanguageI__534D60F1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07443AE0CF");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events__3214EC07F8B02AAA");

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.EventName).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(150);

            entity.HasOne(d => d.User).WithMany(p => p.Events)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Events__UserId__6A30C649");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Language__3214EC07EBE90C06");

            entity.Property(e => e.BookLanguage).HasMaxLength(30);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC0762F5AF43");

            entity.Property(e => e.NotificationDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Book).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Notificat__BookI__6EF57B66");

            entity.HasOne(d => d.Event).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Notificat__Event__6FE99F9F");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__6D0D32F4");
        });
        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rating__3214EC0732AC21B4");

            entity.HasOne(d => d.Book).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Rating__BookId__7B264821");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Rating__UserId__7C1A6C5A");
        });
        modelBuilder.Entity<Remender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Remender__3214EC07F99D795E");

            entity.HasOne(d => d.Book).WithMany(p => p.Remenders)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Remenders__BookI__72C60C4A");

            entity.HasOne(d => d.User).WithMany(p => p.Remenders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Remenders__UserI__73BA3083");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rentals__3214EC07486A713F");

            entity.HasOne(d => d.Book).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Rentals__BookId__66603565");

            entity.HasOne(d => d.User).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Rentals__UserId__6754599E");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reports__3214EC0790BD8BA9");

            entity.Property(e => e.GeneratedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ReportType).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reports__UserId__76969D2E");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC079D639DAD");

            entity.Property(e => e.ExpirationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ReservationDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Book).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Reservati__BookI__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reservati__UserI__6383C8BA");
        });

 

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Review__3214EC07AA8544E6");

            entity.Property(e => e.ReviewDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Book).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Review__BookId__76619304");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Review__UserId__7755B73D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC0794AD8251");

            entity.Property(e => e.RoleName).HasMaxLength(30);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07A6E2EFF5");

            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__5812160E");
        });
        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("[PK__UserLogi__3214EC070064523E]");
            entity.Property(e => e.UserLoginDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.UserLogins)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("[FK__UserLogin__UserI__625A9A57]");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
