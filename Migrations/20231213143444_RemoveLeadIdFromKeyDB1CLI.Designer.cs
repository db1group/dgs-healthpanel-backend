﻿// <auto-generated />
using System;
using Db1HealthPanelBack.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    [DbContext(typeof(ContextConfig))]
    [Migration("20231213143444_RemoveLeadIdFromKeyDB1CLI")]
    partial class RemoveLeadIdFromKeyDB1CLI
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
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

                    b.Property<Guid?>("EvaluationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationId")
                        .IsUnique();

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

                    b.Property<Guid?>("AnswerId")
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

            modelBuilder.Entity("Db1HealthPanelBack.Entities.KeyDB1CLI", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Key");

                    b.ToTable("KeysDB1CLI");
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
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("QuantityDevs")
                        .HasColumnType("bigint");

                    b.Property<string>("SonarName")
                        .HasColumnType("text");

                    b.Property<string>("SonarProjectKeys")
                        .HasColumnType("text");

                    b.Property<string>("SonarToken")
                        .HasColumnType("text");

                    b.Property<string>("SonarUrl")
                        .HasColumnType("text");

                    b.Property<bool>("UseDB1CLI")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CostCenterId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.ProjectResponder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsLead")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectResponders");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.QualityGate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("MetricClassification")
                        .HasColumnType("text");

                    b.Property<string>("MetricName")
                        .HasColumnType("text");

                    b.Property<string>("ProjectKey")
                        .HasColumnType("text");

                    b.Property<DateTime>("ScanDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("QualityGates");
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

            modelBuilder.Entity("Db1HealthPanelBack.Entities.SonarMetric", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Domain")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Key");

                    b.ToTable("SonarMetrics");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.SonarReadingDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MetricKey")
                        .HasColumnType("text");

                    b.Property<long>("ReadingId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SonarReadingDetails");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Stack", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Stacks");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.StackProject", b =>
                {
                    b.Property<string>("StackId")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.HasKey("StackId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("StackProjects");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Answer", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.Evaluation", "Evaluation")
                        .WithOne("Answer")
                        .HasForeignKey("Db1HealthPanelBack.Entities.Answer", "EvaluationId");

                    b.Navigation("Evaluation");
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

            modelBuilder.Entity("Db1HealthPanelBack.Entities.ProjectResponder", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.Project", "Project")
                        .WithMany("ProjectResponders")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Question", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.Column", null)
                        .WithMany("Questions")
                        .HasForeignKey("ColumnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.StackProject", b =>
                {
                    b.HasOne("Db1HealthPanelBack.Entities.Project", "Project")
                        .WithMany("StackProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Db1HealthPanelBack.Entities.Stack", "Stack")
                        .WithMany("StackProjects")
                        .HasForeignKey("StackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Stack");
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

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Evaluation", b =>
                {
                    b.Navigation("Answer");
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

                    b.Navigation("ProjectResponders");

                    b.Navigation("StackProjects");
                });

            modelBuilder.Entity("Db1HealthPanelBack.Entities.Stack", b =>
                {
                    b.Navigation("StackProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
