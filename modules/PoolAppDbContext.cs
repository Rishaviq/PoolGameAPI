﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace PoolGameAPI.modules;

public partial class PoolAppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public PoolAppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public PoolAppDbContext()
    {
        
    }

    public PoolAppDbContext(DbContextOptions<PoolAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GameRecord> GameRecords { get; set; }

    public virtual DbSet<ResultType> ResultTypes { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
        .UseMySql(_configuration["SQL:connection"],
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"),
            options=>options.EnableRetryOnFailure()

            );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<GameRecord>(entity =>
        {
            entity.HasKey(e => e.IdgameRecords).HasName("PRIMARY");

            entity.ToTable("game_records");

            entity.HasIndex(e => e.GameRecordsPlayer, "player_id_idx");

            entity.HasIndex(e => e.GameRecordsResult, "results_idx");

            entity.Property(e => e.IdgameRecords)
                .ValueGeneratedNever()
                .HasColumnName("idgame_records");
            entity.Property(e => e.GameRecordsBestStreak).HasColumnName("game_records_best_streak");
            entity.Property(e => e.GameRecordsFouls).HasColumnName("game_records_fouls");
            entity.Property(e => e.GameRecordsGameId)
                .HasMaxLength(45)
                .HasColumnName("game_records_GameId");
            entity.Property(e => e.GameRecordsHandball).HasColumnName("game_records_handball");
            entity.Property(e => e.GameRecordsPlayer).HasColumnName("game_records_player");
            entity.Property(e => e.GameRecordsResult).HasColumnName("game_records_result");
            entity.Property(e => e.GameRecordsShotAttempted).HasColumnName("game_records_shot_attempted");
            entity.Property(e => e.GameRecordsShotsMade).HasColumnName("game_records_shots_made");

            entity.HasOne(d => d.GameRecordsPlayerNavigation).WithMany(p => p.GameRecords)
                .HasForeignKey(d => d.GameRecordsPlayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("player_id");

            entity.HasOne(d => d.GameRecordsResultNavigation).WithMany(p => p.GameRecords)
                .HasForeignKey(d => d.GameRecordsResult)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("results");
        });

        modelBuilder.Entity<ResultType>(entity =>
        {
            entity.HasKey(e => e.IdresultTypes).HasName("PRIMARY");

            entity.ToTable("result_types");

            entity.Property(e => e.IdresultTypes)
                .ValueGeneratedNever()
                .HasColumnName("idresult_types");
            entity.Property(e => e.ResultType1)
                .HasMaxLength(45)
                .HasColumnName("result_type");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.IduserAccounts).HasName("PRIMARY");

            entity.ToTable("user_accouts");

            entity.HasIndex(e => e.UserAccountsUsername, "user_accouts_username_UNIQUE").IsUnique();

            entity.Property(e => e.IduserAccounts).HasColumnName("iduser_accouts");
            entity.Property(e => e.UserAccountsPassword)
                .HasMaxLength(100)
                .HasColumnName("user_accouts_password");
            entity.Property(e => e.UserAccountsUsername)
                .HasMaxLength(45)
                .HasColumnName("user_accouts_username");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.IdUserProfile).HasName("PRIMARY");

            entity.ToTable("user_profile");

            entity.HasIndex(e => e.UserProfileIduser, "idUser_idx");

            entity.Property(e => e.IdUserProfile)
                .ValueGeneratedNever()
                .HasColumnName("idUser_profile");
            entity.Property(e => e.UserProfileDisplayname)
                .HasMaxLength(45)
                .HasColumnName("User_profile_displayname");
            entity.Property(e => e.UserProfileIduser).HasColumnName("User_profile_iduser");

            entity.HasOne(d => d.UserProfileIduserNavigation).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserProfileIduser)
                .HasConstraintName("idUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
