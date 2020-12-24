﻿// <auto-generated />
using System;
using LifeCMS.Services.ContentCreation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContentCreation.Infrastructure.Migrations
{
    [DbContext(typeof(ContentCreationDbContext))]
    partial class ContentCreationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate.Album", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.HasKey("Id");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AlbumId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Caption")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate.Audience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Audiences");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate.Subscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AudienceId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("SubscribedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SubscribedIpAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SubscriberToken")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("UnsubscribedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UnsubscribedIpAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.HasKey("Id");

                    b.HasIndex("AudienceId");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate.Campaign", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("NewsletterTemplateId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ScheduledDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<bool>("UseSubscriberTimeZone")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate.Newsletter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Newsletters");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(140) CHARACTER SET utf8mb4")
                        .HasMaxLength(140);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate.PostState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("PostStates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "drafted"
                        },
                        new
                        {
                            Id = 2,
                            Name = "published"
                        });
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AvatarImageUri")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Bio")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("HeaderImageUri")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Occupation")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate.Photo", b =>
                {
                    b.HasOne("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AlbumAggregate.Album", null)
                        .WithMany("Photos")
                        .HasForeignKey("AlbumId");
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate.Subscriber", b =>
                {
                    b.HasOne("LifeCMS.Services.ContentCreation.Domain.AggregateModels.AudienceAggregate.Audience", null)
                        .WithMany("Subscribers")
                        .HasForeignKey("AudienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("LifeCMS.Services.ContentCreation.Domain.Common.EmailAddress", "EmailAddress", b1 =>
                        {
                            b1.Property<Guid>("SubscriberId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnName("EmailAddress")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("SubscriberId");

                            b1.ToTable("Subscribers");

                            b1.WithOwner()
                                .HasForeignKey("SubscriberId");
                        });
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate.Campaign", b =>
                {
                    b.OwnsOne("LifeCMS.Services.ContentCreation.Domain.AggregateModels.CampaignAggregate.Subject", "Subject", b1 =>
                        {
                            b1.Property<Guid>("CampaignId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("PreviewText")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.Property<string>("SubjectLineText")
                                .IsRequired()
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("CampaignId");

                            b1.ToTable("Campaigns");

                            b1.WithOwner()
                                .HasForeignKey("CampaignId");
                        });
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate.Newsletter", b =>
                {
                    b.OwnsOne("LifeCMS.Services.ContentCreation.Domain.AggregateModels.NewsletterAggregate.NewsletterBody", "Body", b1 =>
                        {
                            b1.Property<Guid>("NewsletterId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("DesignSource")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.Property<string>("Html")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("NewsletterId");

                            b1.ToTable("Newsletters");

                            b1.WithOwner()
                                .HasForeignKey("NewsletterId");
                        });
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate.Post", b =>
                {
                    b.HasOne("LifeCMS.Services.ContentCreation.Domain.AggregateModels.PostAggregate.PostState", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LifeCMS.Services.ContentCreation.Domain.AggregateModels.UserProfileAggregate.UserProfile", b =>
                {
                    b.OwnsOne("LifeCMS.Services.ContentCreation.Domain.Common.EmailAddress", "EmailAddress", b1 =>
                        {
                            b1.Property<Guid>("UserProfileId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .HasColumnName("EmailAddress")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("UserProfileId");

                            b1.ToTable("UserProfiles");

                            b1.WithOwner()
                                .HasForeignKey("UserProfileId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
