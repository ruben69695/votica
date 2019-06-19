﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Votica.EntityFrameworkCore;

namespace Votica.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(VoticaDbContext))]
    partial class VoticaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Votica.Domain.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(60)")
                        .HasMaxLength(60);

                    b.Property<int?>("QuestionId");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("QuestionId");

                    b.ToTable("options");
                });

            modelBuilder.Entity("Votica.Domain.Participant", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("email")
                        .HasColumnType("varchar(60)");

                    b.HasKey("Email")
                        .HasName("email");

                    b.ToTable("participants");
                });

            modelBuilder.Entity("Votica.Domain.ParticipantOption", b =>
                {
                    b.Property<string>("ParticipantEmail");

                    b.Property<int>("OptionId");

                    b.HasKey("ParticipantEmail", "OptionId");

                    b.HasIndex("OptionId");

                    b.ToTable("optionsPerParticipant");
                });

            modelBuilder.Entity("Votica.Domain.Poll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnName("creationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTimeOffset>("ExpirationDate")
                        .HasColumnName("expirationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id")
                        .HasName("id");

                    b.ToTable("polls");
                });

            modelBuilder.Entity("Votica.Domain.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("PollId");

                    b.Property<int>("TypeId");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("PollId");

                    b.HasIndex("TypeId");

                    b.ToTable("questions");
                });

            modelBuilder.Entity("Votica.Domain.QuestionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(35)")
                        .HasMaxLength(35);

                    b.HasKey("Id")
                        .HasName("id");

                    b.ToTable("questionTypes");
                });

            modelBuilder.Entity("Votica.Domain.Option", b =>
                {
                    b.HasOne("Votica.Domain.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("Votica.Domain.ParticipantOption", b =>
                {
                    b.HasOne("Votica.Domain.Option", "Option")
                        .WithMany("Participants")
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Votica.Domain.Participant", "Participant")
                        .WithMany("Options")
                        .HasForeignKey("ParticipantEmail")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Votica.Domain.Question", b =>
                {
                    b.HasOne("Votica.Domain.Poll", "Poll")
                        .WithMany("Questions")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Votica.Domain.QuestionType", "Type")
                        .WithMany("Questions")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
