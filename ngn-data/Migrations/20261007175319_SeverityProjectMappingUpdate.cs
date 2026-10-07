using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngn_data.Migrations
{
    /// <inheritdoc />
    public partial class SeverityProjectMappingUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "SeverityMasters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SeverityMasters_ProjectId",
                table: "SeverityMasters",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeverityMasters_ProjectMasters_ProjectId",
                table: "SeverityMasters",
                column: "ProjectId",
                principalTable: "ProjectMasters",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeverityMasters_ProjectMasters_ProjectId",
                table: "SeverityMasters");

            migrationBuilder.DropIndex(
                name: "IX_SeverityMasters_ProjectId",
                table: "SeverityMasters");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "SeverityMasters");
        }
    }
}
