using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenClinic_Repository.Migrations
{
    /// <inheritdoc />
    public partial class usercunsultationstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "consultation_status",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "consultation_status",
                table: "Users");
        }
    }
}
