using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BVBM.API.Migrations
{
    public partial class addIsValidatedtoReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValidated",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValidated",
                table: "Reviews");
        }
    }
}
