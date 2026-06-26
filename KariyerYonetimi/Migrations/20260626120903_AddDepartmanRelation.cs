using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KariyerYonetimi.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmanRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmanId",
                table: "Personeller",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departmanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmanlar", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_DepartmanId",
                table: "Personeller",
                column: "DepartmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personeller_Departmanlar_DepartmanId",
                table: "Personeller",
                column: "DepartmanId",
                principalTable: "Departmanlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personeller_Departmanlar_DepartmanId",
                table: "Personeller");

            migrationBuilder.DropTable(
                name: "Departmanlar");

            migrationBuilder.DropIndex(
                name: "IX_Personeller_DepartmanId",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "DepartmanId",
                table: "Personeller");
        }
    }
}
