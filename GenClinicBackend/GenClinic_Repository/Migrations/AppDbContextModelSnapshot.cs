﻿// <auto-generated />
using System;
using GenClinic_Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GenClinic_Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GenClinic_Entities.DataModels.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar")
                        .HasColumnName("address");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("avatar");

                    b.Property<int?>("ConsultationStatus")
                        .HasColumnType("int")
                        .HasColumnName("consultation_status");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("created_on");

                    b.Property<DateTimeOffset?>("DOB")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("dob");

                    b.Property<long?>("DoctorId")
                        .HasColumnType("bigint")
                        .HasColumnName("doctor_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar")
                        .HasColumnName("email");

                    b.Property<DateTimeOffset?>("ExpiryTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("expiry_time");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar")
                        .HasColumnName("first_name");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("gender");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<long?>("LabId")
                        .HasColumnType("bigint")
                        .HasColumnName("lab_id");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar")
                        .HasColumnName("last_name");

                    b.Property<string>("OTP")
                        .HasMaxLength(6)
                        .HasColumnType("varchar")
                        .HasColumnName("otp");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar")
                        .HasColumnName("phone_number");

                    b.Property<int>("Role")
                        .HasColumnType("int")
                        .HasColumnName("role");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("updated_on");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DoctorId");

                    b.HasIndex("LabId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GenClinic_Entities.DataModels.User", b =>
                {
                    b.HasOne("GenClinic_Entities.DataModels.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GenClinic_Entities.DataModels.User", "DoctorUsers")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GenClinic_Entities.DataModels.User", "LabUsers")
                        .WithMany()
                        .HasForeignKey("LabId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GenClinic_Entities.DataModels.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("UpdatedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreatedByUser");

                    b.Navigation("DoctorUsers");

                    b.Navigation("LabUsers");

                    b.Navigation("UpdatedByUser");
                });
#pragma warning restore 612, 618
        }
    }
}
