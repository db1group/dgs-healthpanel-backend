﻿// <auto-generated />
using System;
using Db1HealthPanelBack.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    [DbContext(typeof(ContextConfig))]
    partial class ContextConfigModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AccrualMonth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.AnswerPillar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalData")
                        .HasColumnType("text");

                    b.Property<Guid?>("AnswerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("PillarId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("PillarId");

                    b.ToTable("AnswerPillars");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.AnswerQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AnswerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("AnswersQuestions");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Column", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalData")
                        .HasColumnType("text");

                    b.Property<int?>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid>("PillarId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("PillarId");

                    b.ToTable("Column");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.CostCenter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CostCenters");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Evaluation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("MetricsHealthScore")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ProcessHealthScore")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Lead", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool?>("InTraining")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Leads");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.LeadProject", b =>
                {
                    b.Property<Guid>("LeadId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("LeadId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("LeadProject");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Pillar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BackgroundColor")
                        .HasColumnType("text");

                    b.Property<int?>("Order")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Pillars");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalData")
                        .HasColumnType("text");

                    b.Property<Guid>("CostCenterId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long?>("QuantityDevs")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CostCenterId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ColumnId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("Value")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ColumnId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.AnswerPillar", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.Answer", null)
                        .WithMany("Pillars")
                        .HasForeignKey("AnswerId");

                    b.HasOne("Db1HealthPanelBack.Entities.Pillar", "Pillar")
                        .WithMany()
                        .HasForeignKey("PillarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pillar");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.AnswerQuestion", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.Answer", null)
                        .WithMany("Questions")
                        .HasForeignKey("AnswerId");

                    b.HasOne("Db1HealthPanelBack.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Column", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.Pillar", null)
                        .WithMany("Columns")
                        .HasForeignKey("PillarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Evaluation", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.Project", "Project")
                        .WithMany("Evaluations")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.LeadProject", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.Lead", "Lead")
                        .WithMany("LeadProjects")
                        .HasForeignKey("LeadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Db1HealthPanelBack.Entities.Project", "Project")
                        .WithMany("LeadProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lead");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Project", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.CostCenter", "CostCenter")
                        .WithMany("Projects")
                        .HasForeignKey("CostCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CostCenter");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Question", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.Column", null)
                        .WithMany("Questions")
                        .HasForeignKey("ColumnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Answer", b =>
                {
                    b.Navigation("Pillars");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Column", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.CostCenter", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Lead", b =>
                {
                    b.Navigation("LeadProjects");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Pillar", b =>
                {
                    b.Navigation("Columns");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Project", b =>
                {
                    b.Navigation("Evaluations");

                    b.Navigation("LeadProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
