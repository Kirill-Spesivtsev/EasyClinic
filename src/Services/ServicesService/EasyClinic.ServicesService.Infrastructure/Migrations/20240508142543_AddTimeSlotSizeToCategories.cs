using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyClinic.ServicesService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeSlotSizeToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSlotSize",
                table: "ServiceCategory",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSlotSize",
                table: "ServiceCategory");
        }
    }
}
