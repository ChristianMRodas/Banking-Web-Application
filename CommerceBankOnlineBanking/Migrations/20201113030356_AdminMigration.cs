using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommerceBankOnlineBanking.Migrations
{
    public partial class AdminMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("5dd5f655-7b59-4cd5-afa0-6b1aa6d32f40"), "Admin", "Pass", "AMbqsSq9IYM8s65McgzZ8Y00JtN/m7LkhPHQDTPSxOWADLAhsvgMUbKJlUPnNtI4+Q==", "AdminPass" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("5dd5f655-7b59-4cd5-afa0-6b1aa6d32f40"));
        }
    }
}
