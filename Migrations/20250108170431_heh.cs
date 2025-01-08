using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voxerra_API.Migrations
{
    /// <inheritdoc />
    public partial class heh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TblUsers",
                table: "TblUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblUserFriends",
                table: "TblUserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblMessages",
                table: "TblMessages");

            migrationBuilder.RenameTable(
                name: "TblUsers",
                newName: "Tblusers");

            migrationBuilder.RenameTable(
                name: "TblUserFriends",
                newName: "Tbluserfriends");

            migrationBuilder.RenameTable(
                name: "TblMessages",
                newName: "Tblmessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tblusers",
                table: "Tblusers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tbluserfriends",
                table: "Tbluserfriends",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tblmessages",
                table: "Tblmessages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tblpendingusers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StoredSalt = table.Column<byte[]>(type: "longblob", nullable: false),
                    VerificationCode = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tblpendingusers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tblpendingusers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tblusers",
                table: "Tblusers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tbluserfriends",
                table: "Tbluserfriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tblmessages",
                table: "Tblmessages");

            migrationBuilder.RenameTable(
                name: "Tblusers",
                newName: "TblUsers");

            migrationBuilder.RenameTable(
                name: "Tbluserfriends",
                newName: "TblUserFriends");

            migrationBuilder.RenameTable(
                name: "Tblmessages",
                newName: "TblMessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblUsers",
                table: "TblUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblUserFriends",
                table: "TblUserFriends",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblMessages",
                table: "TblMessages",
                column: "Id");
        }
    }
}
