using Microsoft.EntityFrameworkCore.Migrations;

namespace FaceBookAPP.Migrations
{
    public partial class addedUserPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IstifadeciId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userPosts_AspNetUsers_IstifadeciId",
                        column: x => x.IstifadeciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userPosts_IstifadeciId",
                table: "userPosts",
                column: "IstifadeciId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userPosts");
        }
    }
}
