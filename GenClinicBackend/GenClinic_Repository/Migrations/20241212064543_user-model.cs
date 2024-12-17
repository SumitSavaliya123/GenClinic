using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenClinic_Repository.Migrations
{
    /// <inheritdoc />
    public partial class usermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    last_name = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    email = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    address = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false),
                    dob = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    role = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    doctor_id = table.Column<long>(type: "bigint", nullable: true),
                    lab_id = table.Column<long>(type: "bigint", nullable: true),
                    avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    otp = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    expiry_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    updated_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_lab_id",
                        column: x => x.lab_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_updated_by",
                        column: x => x.updated_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_created_by",
                table: "Users",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Users_doctor_id",
                table: "Users",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_lab_id",
                table: "Users",
                column: "lab_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_updated_by",
                table: "Users",
                column: "updated_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
