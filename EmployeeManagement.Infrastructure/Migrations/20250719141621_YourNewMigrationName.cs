using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmployeeManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class YourNewMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_Companies_CompanyId",
                table: "CompanyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_Companies_CompanyId1",
                table: "CompanyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_Users_UserId",
                table: "CompanyUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyUser",
                table: "CompanyUser");

            migrationBuilder.DropIndex(
                name: "IX_CompanyUser_CompanyId1",
                table: "CompanyUser");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "CompanyUser");

            migrationBuilder.RenameTable(
                name: "CompanyUser",
                newName: "CompanyUsers");

            migrationBuilder.RenameColumn(
                name: "JoinedDate",
                table: "CompanyUsers",
                newName: "JoinedAt");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyUser_CompanyId",
                table: "CompanyUsers",
                newName: "IX_CompanyUsers_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyUsers",
                table: "CompanyUsers",
                columns: new[] { "UserId", "CompanyId" });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: true),
                    Token = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    InvitedById = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitations_Users_InvitedById",
                        column: x => x.InvitedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_CompanyId",
                table: "Invitations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Email",
                table: "Invitations",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_InvitedById",
                table: "Invitations",
                column: "InvitedById");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Token",
                table: "Invitations",
                column: "Token",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUsers_Companies_CompanyId",
                table: "CompanyUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUsers_Users_UserId",
                table: "CompanyUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUsers_Companies_CompanyId",
                table: "CompanyUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUsers_Users_UserId",
                table: "CompanyUsers");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyUsers",
                table: "CompanyUsers");

            migrationBuilder.RenameTable(
                name: "CompanyUsers",
                newName: "CompanyUser");

            migrationBuilder.RenameColumn(
                name: "JoinedAt",
                table: "CompanyUser",
                newName: "JoinedDate");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyUsers_CompanyId",
                table: "CompanyUser",
                newName: "IX_CompanyUser_CompanyId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId1",
                table: "CompanyUser",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyUser",
                table: "CompanyUser",
                columns: new[] { "UserId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_CompanyId1",
                table: "CompanyUser",
                column: "CompanyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_Companies_CompanyId",
                table: "CompanyUser",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_Companies_CompanyId1",
                table: "CompanyUser",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_Users_UserId",
                table: "CompanyUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
