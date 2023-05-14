using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plugins.DataStore.SQL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsManager = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreationUserName = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdateUserName = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreationUserName = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdateUserName = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: true),
                    CreationUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(dateadd(hour,(3),getutcdate()))"),
                    LastUpdateUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeave_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeave_EmployeeId",
                table: "EmployeeLeave",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLeave");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
