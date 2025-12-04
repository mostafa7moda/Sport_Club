using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sport_Club.Models;

namespace Sport_Club.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        // DbSets
        public DbSet<Department> Departments { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberSection> MemberSections { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionResult> CompetitionResults { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ---------- ApplicationUser ----------
            builder.Entity<ApplicationUser>(b =>
            {
                b.Property(u => u.FullName).HasMaxLength(200).IsRequired(false);
                b.Property(u => u.Gender).HasMaxLength(20).IsRequired(false);
            });

            // ---------- Department ----------
            builder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.DepartmentId);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(200);
                entity.Property(d => d.Description).HasMaxLength(1000).IsRequired(false);

                entity.HasOne(d => d.Manager)
                      .WithMany()
                      .HasForeignKey(d => d.ManagerId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(d => d.Sections)
                      .WithOne(s => s.Department)
                      .HasForeignKey(s => s.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Teams)
                      .WithOne(t => t.Department)
                      .HasForeignKey(t => t.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(d => d.Name).HasDatabaseName("IX_Department_Name");
            });

            // ---------- Section ----------
            builder.Entity<Section>(entity =>
            {
                entity.HasKey(s => s.SectionId);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(250);
                entity.Property(s => s.Shift).HasMaxLength(100);
                entity.Property(s => s.Gender).HasMaxLength(20);

                entity.HasOne(s => s.Manager)
                      .WithMany()
                      .HasForeignKey(s => s.ManagerId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(s => s.Trainers)
                      .WithOne(t => t.Section)
                      .HasForeignKey(t => t.SectionId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(s => s.MemberSections)
                      .WithOne(ms => ms.Section)
                      .HasForeignKey(ms => ms.SectionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(s => s.Competitions)
                      .WithOne(c => c.Section)
                      .HasForeignKey(c => c.SectionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(s => new { s.DepartmentId, s.Name }).HasDatabaseName("IX_Section_Department_Name");
            });

            // ---------- Trainer ----------
            builder.Entity<Trainer>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Gender).HasMaxLength(20);
                entity.Property(t => t.Shift).HasMaxLength(100);

                entity.HasOne(t => t.User)
                      .WithMany(u => u.Trainers)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(t => t.Section)
                      .WithMany(s => s.Trainers)
                      .HasForeignKey(t => t.SectionId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(t => t.UserId).HasDatabaseName("IX_Trainer_User");
            });

            // ---------- Member ----------
            builder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.MemberId);
                entity.Property(m => m.EmergencyPhone).HasMaxLength(50);
                entity.Property(m => m.HealthNotes).HasMaxLength(2000);
                entity.Property(m => m.JoinDate).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(m => m.User)
                      .WithMany(u => u.Members)
                      .HasForeignKey(m => m.UserId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(m => m.MemberSections)
                      .WithOne(ms => ms.Member)
                      .HasForeignKey(ms => ms.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(m => m.CompetitionResults)
                      .WithOne(r => r.Member)
                      .HasForeignKey(r => r.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(m => m.TeamMembers)
                      .WithOne(tm => tm.Member)
                      .HasForeignKey(tm => tm.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(m => m.UserId).HasDatabaseName("IX_Member_User");
            });

            // ---------- MemberSection ----------
            builder.Entity<MemberSection>(entity =>
            {
                entity.HasKey(ms => ms.Id);

                entity.Property(ms => ms.Status).HasMaxLength(50);
                entity.Property(ms => ms.StartDate).HasColumnType("datetime2");
                entity.Property(ms => ms.EndDate).HasColumnType("datetime2");

                entity.HasOne(ms => ms.Member)
                      .WithMany(m => m.MemberSections)
                      .HasForeignKey(ms => ms.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ms => ms.Section)
                      .WithMany(s => s.MemberSections)
                      .HasForeignKey(ms => ms.SectionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(ms => new { ms.MemberId, ms.SectionId })
                      .IsUnique()
                      .HasDatabaseName("UX_MemberSection_Member_Section");
            });

            // ---------- Subscription (One-to-One) ----------
            builder.Entity<Subscription>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Price).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(s => s.PaymentStatus).HasMaxLength(50);
                entity.Property(s => s.StartDate).HasColumnType("datetime2");
                entity.Property(s => s.EndDate).HasColumnType("datetime2");

                entity.HasOne(s => s.MemberSection)
                      .WithOne(ms => ms.Subscription)
                      .HasForeignKey<Subscription>(s => s.MemberSectionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(s => s.MemberSectionId)
                      .IsUnique()
                      .HasDatabaseName("UX_Subscription_MemberSection");
            });

            // ---------- Attendance ----------
            builder.Entity<Attendance>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Status).HasMaxLength(50).IsRequired();
                entity.Property(a => a.Date).HasColumnType("datetime2").IsRequired();

                entity.HasOne(a => a.Member)
                      .WithMany(m => m.Attendance)
                      .HasForeignKey(a => a.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(a => new { a.MemberId, a.SectionId, a.Date })
                      .HasDatabaseName("IX_Attendance_Member_Section_Date");
            });

            // ---------- Team ----------
            builder.Entity<Team>(entity =>
            {
                entity.HasKey(t => t.TeamId);
                entity.Property(t => t.TeamName).HasMaxLength(200).IsRequired();

                entity.HasOne(t => t.Department)
                      .WithMany(d => d.Teams)
                      .HasForeignKey(t => t.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.Coach)
                      .WithMany()
                      .HasForeignKey(t => t.CoachId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(t => new { t.DepartmentId, t.TeamName })
                      .HasDatabaseName("IX_Team_Department_Name");
            });

            // ---------- TeamMember ----------
            builder.Entity<TeamMember>(entity =>
            {
                entity.HasKey(tm => tm.TeamMemberId);

                entity.Property(tm => tm.JoinDate).HasColumnType("datetime2");

                entity.HasOne(tm => tm.Team)
                      .WithMany(t => t.TeamMembers)
                      .HasForeignKey(tm => tm.TeamId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tm => tm.Member)
                      .WithMany(m => m.TeamMembers)
                      .HasForeignKey(tm => tm.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(tm => new { tm.TeamId, tm.MemberId })
                      .IsUnique()
                      .HasDatabaseName("UX_TeamMember_Team_Member");
            });

            // ---------- Competition ----------
            builder.Entity<Competition>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Title).HasMaxLength(300).IsRequired();
                entity.Property(c => c.Description).HasMaxLength(2000);
                entity.Property(c => c.StartDate).HasColumnType("datetime2");

                entity.HasOne(c => c.Section)
                      .WithMany(s => s.Competitions)
                      .HasForeignKey(c => c.SectionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(c => new { c.SectionId, c.StartDate })
                      .HasDatabaseName("IX_Competition_Section_StartDate");
            });

            // ---------- CompetitionResult ----------
            builder.Entity<CompetitionResult>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Position).IsRequired();
                entity.Property(r => r.Score).IsRequired();
                entity.Property(r => r.Notes).HasMaxLength(2000);

                entity.HasOne(r => r.Competition)
                      .WithMany(c => c.Results)
                      .HasForeignKey(r => r.CompetitionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Member)
                      .WithMany(m => m.CompetitionResults)
                      .HasForeignKey(r => r.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(r => new { r.CompetitionId, r.MemberId })
                      .HasDatabaseName("IX_CompetitionResult_Competition_Member");
            });
        }
    }
}
