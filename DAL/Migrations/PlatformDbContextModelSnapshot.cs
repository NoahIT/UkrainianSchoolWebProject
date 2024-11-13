﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(PlatformDbContext))]
    partial class PlatformDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DAL.Models.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");
                });

            modelBuilder.Entity("DAL.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstPassword")
                        .HasColumnType("longtext");

                    b.Property<string>("Login")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("DAL.Models.AppUserAppRole", b =>
                {
                    b.Property<int?>("AppUserId")
                        .HasColumnType("int");

                    b.Property<int?>("AppRoleId")
                        .HasColumnType("int");

                    b.HasKey("AppUserId", "AppRoleId");

                    b.HasIndex("AppRoleId");

                    b.ToTable("AppUserAppRole");
                });

            modelBuilder.Entity("DAL.Models.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("DAL.Models.DbSessionModel", b =>
                {
                    b.Property<Guid>("DbSessionId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastAccessed")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SessionData")
                        .HasColumnType("longtext");

                    b.HasKey("DbSessionId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("DbSessions");
                });

            modelBuilder.Entity("DAL.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("DAL.Models.Lessons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateOfLesson")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Source")
                        .HasColumnType("longtext");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("DAL.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("GradeClass")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SubscriptionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("isValid")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DAL.Models.OrderSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("SubjectId");

                    b.ToTable("OrderSubjects");
                });

            modelBuilder.Entity("DAL.Models.PasswordCodes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<Guid>("VerificationCode")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PasswordCodes");
                });

            modelBuilder.Entity("DAL.Models.PaymentToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RecToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentTokens");
                });

            modelBuilder.Entity("DAL.Models.PaymentsHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AcquirerBankName")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("AuthCode")
                        .HasColumnType("longtext");

                    b.Property<string>("CardPan")
                        .HasColumnType("longtext");

                    b.Property<string>("CardProduct")
                        .HasColumnType("longtext");

                    b.Property<string>("CardType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClientName")
                        .HasColumnType("longtext");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedHistory")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Currency")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IssuerBankCountry")
                        .HasColumnType("longtext");

                    b.Property<string>("IssuerBankName")
                        .HasColumnType("longtext");

                    b.Property<string>("MerchantAccount")
                        .HasColumnType("longtext");

                    b.Property<string>("MerchantSignature")
                        .HasColumnType("longtext");

                    b.Property<string>("OrderReference")
                        .HasColumnType("longtext");

                    b.Property<string>("PaymentSystem")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<long>("ProcessingDate")
                        .HasColumnType("bigint");

                    b.Property<string>("Reason")
                        .HasColumnType("longtext");

                    b.Property<int>("ReasonCode")
                        .HasColumnType("int");

                    b.Property<string>("RecToken")
                        .HasColumnType("longtext");

                    b.Property<string>("TransactionStatus")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PaymentsHistories");
                });

            modelBuilder.Entity("DAL.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("DAL.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("DAL.Models.StreamInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AbbrName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Identifier")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsLive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StreamPath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("StreamInformations");
                });

            modelBuilder.Entity("DAL.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AbbrSubject")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("GradeClass")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("DAL.Models.SubscriptionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discount")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("FirstPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("LessonsCount")
                        .HasColumnType("int");

                    b.Property<int>("Months")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionTypes");
                });

            modelBuilder.Entity("DAL.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("ExampleVideo")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Photo")
                        .HasColumnType("longtext");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Models.UserGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroup");
                });

            modelBuilder.Entity("DAL.Models.UserToken", b =>
                {
                    b.Property<Guid>("UserTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("DAL.Models.Videos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Where")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("DAL.Models.AppUserAppRole", b =>
                {
                    b.HasOne("DAL.Models.AppRole", "AppRole")
                        .WithMany()
                        .HasForeignKey("AppRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppRole");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("DAL.Models.Coupon", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.DbSessionModel", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Lessons", b =>
                {
                    b.HasOne("DAL.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("DAL.Models.Order", b =>
                {
                    b.HasOne("DAL.Models.SubscriptionType", "SubscriptionType")
                        .WithMany()
                        .HasForeignKey("SubscriptionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubscriptionType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.OrderSubject", b =>
                {
                    b.HasOne("DAL.Models.Order", "Order")
                        .WithMany("Subjects")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("DAL.Models.PasswordCodes", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.PaymentToken", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Schedule", b =>
                {
                    b.HasOne("DAL.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("DAL.Models.StreamInformation", b =>
                {
                    b.HasOne("DAL.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("DAL.Models.Teacher", b =>
                {
                    b.HasOne("DAL.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.HasOne("DAL.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("DAL.Models.UserGroup", b =>
                {
                    b.HasOne("DAL.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.UserToken", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Order", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
