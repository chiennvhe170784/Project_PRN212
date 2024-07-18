using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProjectPRN.Models;

public partial class DataPrnquizContext : DbContext
{
    public DataPrnquizContext()
    {
    }

    public DataPrnquizContext(DbContextOptions<DataPrnquizContext> options)
        : base(options)
    {
    }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestQuestion> TestQuestions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__History__4D7B4ABDB730445C");

            entity.ToTable("History");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Mark)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mark");
            entity.Property(e => e.Uid).HasColumnName("uid");

            entity.HasOne(d => d.UidNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.Uid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_User");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Qid);

            entity.ToTable("question");

            entity.Property(e => e.Qid).HasColumnName("qid");
            entity.Property(e => e.Answer)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("answer");
            entity.Property(e => e.CorrectAnswer)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("correctAnswer");
            entity.Property(e => e.Question1)
                .IsUnicode(false)
                .HasColumnName("question");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Test");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.TestName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("test_name");
        });

        modelBuilder.Entity<TestQuestion>(entity =>
        {
            entity.HasKey(e => e.TestQuestionId).HasName("PK__TestQues__4C589E097BFF7B9B");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Qid).HasColumnName("qid");

            entity.HasOne(d => d.CodeNavigation).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.Code)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestQuestions_Test");

            entity.HasOne(d => d.QidNavigation).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.Qid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestQuestions_question");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.ToTable("User");

            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
