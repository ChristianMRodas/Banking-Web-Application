using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommerceBankOnlineBanking.Migrations
{
    public partial class TransactionSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Transaction",
                columns: new[] { "Id", "AccountNumber", "Action", "Amount", "Balance", "Description", "ProcessingDate", "UserId" },
                values: new object[] { new Guid("0ddb4852-9899-4493-8d4d-20c8b19a7fc3"), 1, "Account Open", 0.0, 5000.0, "Starting Balance", new DateTime(2020, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("097aeda9-59c5-4611-b225-99c0cddf68be") });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("ad799726-74aa-4d9a-90b9-fb11b4347aff"), "Admin", "Pass", "AEcoeVZIl1+kCRcj4oYJ7PgwTA/kY9tImvbwMS7kJ79IgvPoQgoQmnpwWaAR8zpdog==", "AdminPass" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("097aeda9-59c5-4611-b225-99c0cddf68be"), "Test", "User", "APbToWW4JNHkNFkqXMVGUdqxEDPBlumQCOA2YT8Yaa6sjKBP3TY2KyHpe8CeY0LAsA==", "TestUser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: new Guid("0ddb4852-9899-4493-8d4d-20c8b19a7fc3"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("097aeda9-59c5-4611-b225-99c0cddf68be"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ad799726-74aa-4d9a-90b9-fb11b4347aff"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("ddd74e6e-4bd3-401f-992c-be72506d46d7"), "Admin", "Pass", "ADngwL/EIIkZ42Ms12NvTaP3icTsg6G3Z5HXfgtVknB16X5pyFNA1crushnt6QkMgg==", "AdminPass" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("ac87ac7a-2e1f-4d88-980d-d15ff9393a4e"), "Test", "User", "AKO7B6Jsaz2tzgoY15CbVpjFoU2W0ywciuDD/9s5ou+dClBin070SCWEJK7V3NYLEA==", "TestUser" });
        }
    }
}
