using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.Models
{
	public class DebateAbleDbContext : DbContext
	{
		public DebateAbleDbContext(DbContextOptions options)
			: base(options)
		{

		}

		public virtual DbSet<AppUser> AppUsers { get; set; }
		public virtual DbSet<Debate> Debates { get; set; }
		public virtual DbSet<DebateComment> DebateComments { get; set; }
		public virtual DbSet<DebateParticipant> DebateParticipants { get; set; }
		public virtual DbSet<DebatePost> DebatePosts { get; set; }
		public virtual DbSet<Invitation> Invitations { get; set; }	
		public virtual DbSet<ParticipantType> ParticipantTypes { get; set; }
		public virtual DbSet<ResponseRequest> ResponseRequests { get; set; }

        public override int SaveChanges()
        {
			TrackChanges();
			return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
			TrackChanges();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
			TrackChanges();
			return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
			TrackChanges();

			return base.SaveChangesAsync(cancellationToken);
        }

		private void TrackChanges()
        {
			var entries = ChangeTracker
				.Entries()
				.Where(e => e.Entity is BaseTrackableModel && (
						e.State == EntityState.Added
						|| e.State == EntityState.Modified));

			foreach (var entityEntry in entries)
			{
				((BaseTrackableModel)entityEntry.Entity).ModifiedOnUtc = DateTime.Now;

				if (entityEntry.State == EntityState.Added)
				{
					((BaseTrackableModel)entityEntry.Entity).CreatedOnUtc = DateTime.Now;
				}
			}
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AppUser>(e =>
			{
				e.ToTable("AppUser");

				e.HasKey(e => e.Id);
				e.Property(e => e.Id)
					.HasColumnType("uniqueidentifier")
					.HasDefaultValueSql("newsequentialid()");
			});

			modelBuilder.Entity<Debate>(e =>
			{
				e.ToTable("Debate");

				e.HasKey(e => e.Id);
				e.Property(e => e.Id)
					.HasColumnType("uniqueidentifier")
					.HasDefaultValueSql("newsequentialid()");

				e.HasOne(e => e.CreatedBy)
					.WithMany(au => au.StartedDebates)
					.HasForeignKey(e => e.CreatedByAppUserId); 
			});

			modelBuilder.Entity<DebateComment>(e =>
			{
				e.ToTable("DebateComment");

				e.HasKey(e => e.Id);
				e.Property(e => e.Id)
					.HasColumnType("uniqueidentifier")
					.HasDefaultValueSql("newsequentialid()");

				e.HasOne(e => e.AppUser)
					.WithMany(au => au.DebateComments)
					.HasForeignKey(e => e.AppUserId);

				e.HasOne(e => e.Debate)
					.WithMany(d => d.Comments)
					.HasForeignKey(e => e.DebateId);
			});

			modelBuilder.Entity<DebateParticipant>(e =>
			{
				e.ToTable("DebateParticipant");

				e.HasKey(e => e.Id);
				e.Property(e => e.Id)
					.HasColumnType("uniqueidentifier")
					.HasDefaultValueSql("newsequentialid()");

				e.HasOne(e => e.AppUser)
					.WithMany(au => au.ParticipantOf)
					.HasForeignKey(e => e.AppUserId);

				e.HasOne(e => e.Debate)
					.WithMany(d => d.Participants)
					.HasForeignKey(e => e.DebateId);
			});

			modelBuilder.Entity<DebatePost>(e =>
			{
				e.ToTable("DebatePost");

				e.HasKey(e => e.Id);
				e.Property(e => e.Id)
					.HasColumnType("uniqueidentifier")
					.HasDefaultValueSql("newsequentialid()");

				e.HasOne(e => e.AppUser)
					.WithMany(e => e.DebatePosts)
					.HasForeignKey(e => e.AppUserId);

				e.HasOne(e => e.Debate)
					.WithMany(d => d.Posts)
					.HasForeignKey(e => e.DebateId);
			});

			modelBuilder.Entity<Invitation>(e =>
			{
				e.ToTable("Invitation");
				e.HasKey(e => e.Id);
				e.Property(e => e.Id)
					.HasColumnType("uniqueidentifier")
					.HasDefaultValueSql("newsequentialid()");

				e.HasOne(e => e.Inviter)
					.WithMany(au => au.SentInvitations)
					.HasForeignKey(e => e.InviterAppUserId)
					.HasConstraintName("FK_Invitation_InviterAppUserId");

				e.HasOne(e => e.Invitee)
					.WithMany(au => au.ReceivedInvitations)
					.HasForeignKey(e => e.InviteeAppUserId)
					.HasConstraintName("FK_Invitation_InviteeAppUserId");
			});

			modelBuilder.Entity<ParticipantType>(e =>
			{
				e.ToTable("ParticipantType");
			});

			modelBuilder.Entity<ResponseRequest>(e =>
			{
				e.ToTable("ResponseRequest");
				e.HasKey(e => e.Id);


			});
		}
	}
}
