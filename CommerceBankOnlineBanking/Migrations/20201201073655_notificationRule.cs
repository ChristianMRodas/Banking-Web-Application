using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommerceBankOnlineBanking.Migrations
{
    public partial class notificationRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "NotificationRule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RuleType = table.Column<string>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRule", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "Id", "AccountNumber", "Action", "Amount", "Balance", "Description", "ProcessingDate", "UserId" },
                values: new object[] { new Guid("53eee79b-40bd-4abd-b36f-6e93fd6d5a64"), 1, "Account Open", 0.0, 5000.0, "Starting Balance", new DateTime(2020, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3d6b7c38-1daf-412d-beb7-04eae0aceff8") });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("5eb63d56-3a0c-48e9-bd5f-1c5370a8f695"), "Admin", "Pass", "AMqt+XRYlYmVhyswIXpMSJfYqw3yACwWwsZl4lKWxLQTRPmEGut9g9AqG2J2B4AK+Q==", "AdminPass" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("3d6b7c38-1daf-412d-beb7-04eae0aceff8"), "Test", "User", "AKATIz42cBo4tGpfyKkknWwiRQgN82ITGcGTrAxxbktqYX1pPChd3rm4BtzjlKzw3w==", "TestUser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationRule");

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: new Guid("53eee79b-40bd-4abd-b36f-6e93fd6d5a64"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("3d6b7c38-1daf-412d-beb7-04eae0aceff8"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("5eb63d56-3a0c-48e9-bd5f-1c5370a8f695"));

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
    }
}
