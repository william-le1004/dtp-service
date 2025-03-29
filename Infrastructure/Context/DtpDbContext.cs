using System;
using System.Collections.Generic;
using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class DtpDbContext : DbContext
{
    public DtpDbContext()
    {
    }

    public DtpDbContext(DbContextOptions<DtpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Destination> Destinations { get; set; }

    public virtual DbSet<DestinationActivity> Destinationactivities { get; set; }

    public virtual DbSet<ExternalTransaction> Externaltransactions { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Imageurl> Imageurls { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketType> Tickettypes { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TourBasketItem> Tourbasketitems { get; set; }

    public virtual DbSet<TourBooking> Tourbookings { get; set; }

    public virtual DbSet<TourDestination> Tourdestinations { get; set; }

    public virtual DbSet<TourSchedule> Tourschedules { get; set; }

    public virtual DbSet<TourScheduleTicket> Tourscheduletickets { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=MYSQL1001.site4now.net;Database=db_ab3495_dtp;Uid=ab3495_dtp;Pwd=dtpct123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroles");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<Aspnetroleclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroleclaims");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetusers");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.CompanyId, "IX_AspNetUsers_CompanyId");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LastModified).HasMaxLength(6);
            entity.Property(e => e.LockoutEnd).HasColumnType("datetime");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Company).WithMany(p => p.Aspnetusers)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_AspNetUsers_Companies_CompanyId");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Aspnetuserrole",
                    r => r.HasOne<Aspnetrole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PRIMARY");
                        j.ToTable("aspnetuserroles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetuserclaims");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserclaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserlogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey }).HasName("PRIMARY");

            entity.ToTable("aspnetuserlogins");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetusertoken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }).HasName("PRIMARY");

            entity.ToTable("aspnetusertokens");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("baskets");

            entity.HasIndex(e => e.UserId, "IX_Baskets_UserId").IsUnique();

            entity.HasOne(d => d.User).WithOne(p => p.Basket)
                .HasForeignKey<Basket>(d => d.UserId)
                .HasConstraintName("FK_Baskets_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("companies");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);
        });

        modelBuilder.Entity<Destination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("destinations");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);
        });

        modelBuilder.Entity<DestinationActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("destinationactivities");

            entity.HasIndex(e => e.TourDestinationId, "IX_DestinationActivities_TourDestinationId");

            entity.Property(e => e.EndTime).HasMaxLength(6);
            entity.Property(e => e.StartTime).HasMaxLength(6);

            entity.HasOne(d => d.TourDestination).WithMany(p => p.DestinationActivities)
                .HasForeignKey(d => d.TourDestinationId)
                .HasConstraintName("FK_DestinationActivities_TourDestinations_TourDestinationId");
        });

        modelBuilder.Entity<ExternalTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("externaltransaction");

            entity.HasIndex(e => e.UserId, "IX_ExternalTransaction_UserId");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);

            entity.HasOne(d => d.User).WithMany(p => p.ExternalTransactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ExternalTransaction_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("feedbacks");

            entity.HasIndex(e => e.TourId, "IX_Feedbacks_TourId");

            entity.HasIndex(e => e.UserId, "IX_Feedbacks_UserId");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);

            entity.HasOne(d => d.Tour).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK_Feedbacks_Tours_TourId");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Feedbacks_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Imageurl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("imageurls");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payments");

            entity.HasIndex(e => e.BookingId, "IX_Payments_BookingId");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_Payments_TourBookings_BookingId");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ratings");

            entity.HasIndex(e => e.TourId, "IX_Ratings_TourId");

            entity.HasIndex(e => e.UserId, "IX_Ratings_UserId");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);

            entity.HasOne(d => d.Tour).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK_Ratings_Tours_TourId");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Ratings_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tickets");

            entity.HasIndex(e => e.TicketTypeId, "IX_Tickets_TicketTypeId");

            entity.HasIndex(e => e.TourBookingId, "IX_Tickets_TourBookingId");

            entity.HasOne(d => d.TicketType).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TicketTypeId)
                .HasConstraintName("FK_Tickets_TicketTypes_TicketTypeId");

            entity.HasOne(d => d.TourBooking).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TourBookingId)
                .HasConstraintName("FK_Tickets_TourBookings_TourBookingId");
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tickettypes");

            entity.HasIndex(e => e.TourId, "IX_TicketTypes_TourId");

            entity.HasOne(d => d.Tour).WithMany(p => p.Tickettypes)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK_TicketTypes_Tours_TourId");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tours");

            entity.HasIndex(e => e.CategoryId, "IX_Tours_CategoryId");

            entity.HasIndex(e => e.CompanyId, "IX_Tours_CompanyId");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);

            entity.HasOne(d => d.Category).WithMany(p => p.Tours)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Tours_Categories_CategoryId");

            entity.HasOne(d => d.Company).WithMany(p => p.Tours)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Tours_Companies_CompanyId");
        });

        modelBuilder.Entity<TourBasketItem>(entity =>
        {
            entity.HasKey(e => new { e.TourScheduleId, e.BasketId, e.TicketTypeId }).HasName("PRIMARY");

            entity.ToTable("tourbasketitems");

            entity.HasIndex(e => e.BasketId, "IX_TourBasketItems_BasketId");

            entity.HasOne(d => d.Basket).WithMany(p => p.TourBasketItem)
                .HasForeignKey(d => d.BasketId)
                .HasConstraintName("FK_TourBasketItems_Baskets_BasketId");

            entity.HasOne(d => d.TourSchedule).WithMany(p => p.TourBasketItem)
                .HasForeignKey(d => d.TourScheduleId)
                .HasConstraintName("FK_TourBasketItems_TourSchedules_TourScheduleId");
        });

        modelBuilder.Entity<TourBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tourbookings");

            entity.HasIndex(e => e.TourScheduleId, "IX_TourBookings_TourScheduleId");

            entity.HasIndex(e => e.VoucherCode, "IX_TourBookings_VoucherCode");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);

            entity.HasOne(d => d.TourSchedule).WithMany(p => p.TourBookings)
                .HasForeignKey(d => d.TourScheduleId)
                .HasConstraintName("FK_TourBookings_TourSchedules_TourScheduleId");

            entity.HasOne(d => d.VoucherCodeNavigation).WithMany(p => p.Tourbookings)
                .HasForeignKey(d => d.VoucherCode)
                .HasConstraintName("FK_TourBookings_Voucher_VoucherCode");
        });

        modelBuilder.Entity<TourDestination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tourdestinations");

            entity.HasIndex(e => e.DestinationId, "IX_TourDestinations_DestinationId");

            entity.HasIndex(e => e.TourId, "IX_TourDestinations_TourId");

            entity.Property(e => e.EndTime).HasMaxLength(6);
            entity.Property(e => e.StartTime).HasMaxLength(6);

            entity.HasOne(d => d.Destination).WithMany(p => p.Tourdestinations)
                .HasForeignKey(d => d.DestinationId)
                .HasConstraintName("FK_TourDestinations_Destinations_DestinationId");

            entity.HasOne(d => d.Tour).WithMany(p => p.Tourdestinations)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK_TourDestinations_Tours_TourId");
        });

        modelBuilder.Entity<TourSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tourschedules");

            entity.HasIndex(e => e.TourId, "IX_TourSchedules_TourId");

            entity.Property(e => e.CloseDate).HasMaxLength(6);
            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);
            entity.Property(e => e.OpenDate).HasMaxLength(6);

            entity.HasOne(d => d.Tour).WithMany(p => p.Tourschedules)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK_TourSchedules_Tours_TourId");
        });

        modelBuilder.Entity<TourScheduleTicket>(entity =>
        {
            entity.HasKey(e => new { e.TourScheduleId, e.TicketTypeId }).HasName("PRIMARY");

            entity.ToTable("tourscheduleticket");

            entity.HasIndex(e => e.TicketTypeId, "IX_TourScheduleTicket_TicketTypeId");

            entity.HasOne(d => d.TicketType).WithMany(p => p.TourScheduleTickets)
                .HasForeignKey(d => d.TicketTypeId)
                .HasConstraintName("FK_TourScheduleTicket_TicketTypes_TicketTypeId");

            entity.HasOne(d => d.TourSchedule).WithMany(p => p.TourScheduleTickets)
                .HasForeignKey(d => d.TourScheduleId)
                .HasConstraintName("FK_TourScheduleTicket_TourSchedules_TourScheduleId");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("transactions");

            entity.HasIndex(e => e.WalletId, "IX_Transactions_WalletId");

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);

            entity.HasOne(d => d.Wallet).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.WalletId)
                .HasConstraintName("FK_Transactions_Wallets_WalletId");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PRIMARY");

            entity.ToTable("voucher");

            entity.Property(e => e.ExpiryDate).HasMaxLength(6);
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("wallets");

            entity.HasIndex(e => e.UserId, "IX_Wallets_UserId").IsUnique();

            entity.Property(e => e.CreatedAt).HasMaxLength(6);
            entity.Property(e => e.LastModified).HasMaxLength(6);

            entity.HasOne(d => d.User).WithOne(p => p.Wallet)
                .HasForeignKey<Wallet>(d => d.UserId)
                .HasConstraintName("FK_Wallets_AspNetUsers_UserId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}