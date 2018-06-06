﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blog.Migrations
{
    [DbContext(typeof(BlogContext))]
    partial class BlogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Evaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EvaluatedById");

                    b.Property<int?>("EvaluatedOnId");

                    b.Property<DateTime>("EvaluationTime");

                    b.Property<int?>("PostId");

                    b.Property<int?>("Value");

                    b.HasKey("Id");

                    b.HasIndex("EvaluatedById");

                    b.HasIndex("EvaluatedOnId");

                    b.HasIndex("PostId");

                    b.ToTable("Evaluation");
                });

            modelBuilder.Entity("Domain.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Message");

                    b.Property<int?>("ThreadId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ThreadId");

                    b.ToTable("Post");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Post");
                });

            modelBuilder.Entity("Domain.PostEvaluation", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("EvaluationId");

                    b.HasKey("PostId", "EvaluationId");

                    b.HasIndex("EvaluationId");

                    b.ToTable("PostEvaluation");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("JoinTime");

                    b.Property<string>("Password");

                    b.Property<int>("Rank");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Domain.Thread", b =>
                {
                    b.HasBaseType("Domain.Post");

                    b.Property<string>("Title");

                    b.ToTable("Thread");

                    b.HasDiscriminator().HasValue("Thread");
                });

            modelBuilder.Entity("Domain.Evaluation", b =>
                {
                    b.HasOne("Domain.User", "EvaluatedBy")
                        .WithMany()
                        .HasForeignKey("EvaluatedById");

                    b.HasOne("Domain.User", "EvaluatedOn")
                        .WithMany()
                        .HasForeignKey("EvaluatedOnId");

                    b.HasOne("Domain.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("Domain.Post", b =>
                {
                    b.HasOne("Domain.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Domain.Thread")
                        .WithMany("Comments")
                        .HasForeignKey("ThreadId");
                });

            modelBuilder.Entity("Domain.PostEvaluation", b =>
                {
                    b.HasOne("Domain.Evaluation", "Evaluation")
                        .WithMany()
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
