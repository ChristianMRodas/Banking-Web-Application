using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommerceBankOnlineBanking.Migrations
{
    public partial class TestUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("5dd5f655-7b59-4cd5-afa0-6b1aa6d32f40"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("ddd74e6e-4bd3-401f-992c-be72506d46d7"), "Admin", "Pass", "ADngwL/EIIkZ42Ms12NvTaP3icTsg6G3Z5HXfgtVknB16X5pyFNA1crushnt6QkMgg==", "AdminPass" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("ac87ac7a-2e1f-4d88-980d-d15ff9393a4e"), "Test", "User", "AKO7B6Jsaz2tzgoY15CbVpjFoU2W0ywciuDD/9s5ou+dClBin070SCWEJK7V3NYLEA==", "TestUser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ac87ac7a-2e1f-4d88-980d-d15ff9393a4e"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ddd74e6e-4bd3-401f-992c-be72506d46d7"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("5dd5f655-7b59-4cd5-afa0-6b1aa6d32f40"), "Admin", "Pass", "AMbqsSq9IYM8s65McgzZ8Y00JtN/m7LkhPHQDTPSxOWADLAhsvgMUbKJlUPnNtI4+Q==", "AdminPass" });
        }
    }
}
