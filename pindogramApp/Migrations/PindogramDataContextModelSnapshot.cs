﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using pindogramApp.Entities;

namespace pindogramApp.Migrations
{
    [DbContext(typeof(PindogramDataContext))]
    partial class PindogramDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("pindogramApp.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateAdded");

                    b.Property<int>("MemeId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("MemeId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("pindogramApp.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("pindogramApp.Entities.Meme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<byte[]>("Image");

                    b.Property<bool>("IsApproved");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Memes");
                });

            modelBuilder.Entity("pindogramApp.Entities.MemeRate", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MemeId");

                    b.Property<int>("UserId");

                    b.Property<bool>("isUpvote");

                    b.HasKey("Id");

                    b.HasIndex("MemeId");

                    b.HasIndex("UserId");

                    b.ToTable("MemeRates");
                });

            modelBuilder.Entity("pindogramApp.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<int?>("GroupId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("pindogramApp.Entities.Comment", b =>
                {
                    b.HasOne("pindogramApp.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("pindogramApp.Entities.Meme", "Meme")
                        .WithMany()
                        .HasForeignKey("MemeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("pindogramApp.Entities.Meme", b =>
                {
                    b.HasOne("pindogramApp.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("pindogramApp.Entities.MemeRate", b =>
                {
                    b.HasOne("pindogramApp.Entities.Meme", "Meme")
                        .WithMany()
                        .HasForeignKey("MemeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("pindogramApp.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("pindogramApp.Entities.User", b =>
                {
                    b.HasOne("pindogramApp.Entities.Group", "Group")
                        .WithMany("User")
                        .HasForeignKey("GroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
