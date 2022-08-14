﻿using Microsoft.EntityFrameworkCore;
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
		public virtual DbSet<ParticipantType> ParticipantTypes { get; set; }

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
			});

			modelBuilder.Entity<DebateComment>(e =>
			{
				e.ToTable("DebateComment");

				e.HasKey(e => e.Id);
				e.Property(e => e.Id)
					.HasColumnType("uniqueidentifier")
					.HasDefaultValueSql("newsequentialid()");
			});

			modelBuilder.Entity<DebateParticipant>(e =>
			{
				e.ToTable("DebateParticipant");

				e.HasKey(e => e.Id);
				e.Property(e => e.Id)
					.HasColumnType("uniqueidentifier")
					.HasDefaultValueSql("newsequentialid()");
			});

			modelBuilder.Entity<DebatePost>(e =>
			{
				e.ToTable("DebatePost");

				e.HasKey(e => e.Id);
				e.Property(e => e.Id)
					.HasColumnType("uniqueidentifier")
					.HasDefaultValueSql("newsequentialid()");
			});

			modelBuilder.Entity<ParticipantType>(e =>
			{
				e.ToTable("ParticipantType");
			});
		}
	}
}