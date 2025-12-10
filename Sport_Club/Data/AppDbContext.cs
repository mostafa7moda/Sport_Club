using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sport_Club.Models;
using Sport_Club.Enum;

namespace Sport_Club.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        // DbSets
        public DbSet<Section> Sections { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberSection> MemberSections { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ---------------------------
            // 1️⃣ تخزين الـ ENUMs كـ string
            // ---------------------------

        

            // Trainer enums
            builder.Entity<Trainer>()
                .Property(t => t.Gender)
                .HasConversion<string>();

            builder.Entity<Trainer>()
                .Property(t => t.Shift)
                .HasConversion<string>();

            // MemberSection status (لو enum)
            builder.Entity<MemberSection>()
                .Property(ms => ms.Status)
                .HasConversion<string>();

            // Section enums
            builder.Entity<Section>()
                .Property(s => s.Gender)
                .HasConversion<string>();

            builder.Entity<Section>()
                .Property(s => s.Shift)
                .HasConversion<string>();

            // Attendance status (لو enum)
            builder.Entity<Attendance>()
                .Property(a => a.Status)
                .HasConversion<string>();

            // Subscription
            builder.Entity<Subscription>()
                .Property(s => s.PaymentStatus)
                .HasConversion<string>();


            // ---------------------------
            // 2️⃣ Decimal Precision
            // ---------------------------
            builder.Entity<Subscription>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);


            // ---------------------------
            // 3️⃣ العلاقات Relationships
            // ---------------------------

            // ApplicationUser ↔ Member/Trainer
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Member)
                .WithOne(m => m.User)
                .HasForeignKey<Member>(m => m.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Trainer)
                .WithOne(t => t.User)
                .HasForeignKey<Trainer>(t => t.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Section
            builder.Entity<Section>()
                .HasOne(s => s.Manager)
                .WithMany()
                .HasForeignKey(s => s.ManagerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Section>()
                .HasMany(s => s.Trainers)
                .WithOne(t => t.Section)
                .HasForeignKey(t => t.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Section>()
                .HasMany(s => s.MemberSections)
                .WithOne(ms => ms.Section)
                .HasForeignKey(ms => ms.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Section>()
                .HasMany(s => s.Attendances)
                .WithOne(a => a.Section)
                .HasForeignKey(a => a.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Member relations
            builder.Entity<Member>()
                .HasMany(m => m.MemberSections)
                .WithOne(ms => ms.Member)
                .HasForeignKey(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Member>()
                .HasMany(m => m.Attendances)
                .WithOne(a => a.Member)
                .HasForeignKey(a => a.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Member>()
                .HasMany(m => m.TeamMembers)
                .WithOne(tm => tm.Member)
                .HasForeignKey(tm => tm.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            // Trainer
            builder.Entity<Trainer>()
                .HasOne(t => t.Section)
                .WithMany(s => s.Trainers)
                .HasForeignKey(t => t.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Team
            builder.Entity<Team>()
                .HasOne(t => t.Coach)
                .WithMany()
                .HasForeignKey(t => t.CoachId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Team>()
                .HasMany(t => t.TeamMembers)
                .WithOne(tm => tm.Team)
                .HasForeignKey(tm => tm.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // MemberSection ↔ Subscription (1–1)
            builder.Entity<MemberSection>()
                .HasOne(ms => ms.Subscription)
                .WithOne(s => s.MemberSection)
                .HasForeignKey<Subscription>(s => s.MemberSectionId)
                .OnDelete(DeleteBehavior.Cascade);


            // ---------------------------
            // 4️⃣ Unique Indexes
            // ---------------------------

            builder.Entity<TeamMember>()
                .HasIndex(tm => new { tm.TeamId, tm.MemberId })
                .IsUnique();

            builder.Entity<MemberSection>()
                .HasIndex(ms => new { ms.MemberId, ms.SectionId })
                .IsUnique();

            builder.Entity<Attendance>()
                .HasIndex(a => new { a.MemberId, a.SectionId, a.Date })
                .IsUnique();


            // ---------------------------
            // 5️⃣ تحديد عدد الحروف
            // ---------------------------

            builder.Entity<Attendance>()
                .Property(a => a.Status)
                .HasMaxLength(20);

            builder.Entity<MemberSection>()
                .Property(ms => ms.Status)
                .HasMaxLength(20);

            builder.Entity<Subscription>()
                .Property(s => s.PaymentStatus)
                .HasMaxLength(20);

            builder.Entity<Member>()
                .Property(m => m.EmergencyPhone)
                .HasMaxLength(15);
        }
    }
}
