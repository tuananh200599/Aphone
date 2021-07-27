using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aphone.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DatedCreate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 25, 18, 15, 21, 994, DateTimeKind.Local).AddTicks(6135),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 8, 17, 42, 39, 890, DateTimeKind.Local).AddTicks(1064));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 25, 18, 15, 22, 11, DateTimeKind.Local).AddTicks(1026),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 8, 17, 42, 39, 899, DateTimeKind.Local).AddTicks(4142));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("83936c8b-1a07-4747-9f33-f5a07815267f"), "3b9042ef-8ece-4956-90da-cc75561ca4a3", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("83936c8b-1a07-4747-9f33-f5a07815267f"), new Guid("a01bb0d8-1cc6-4118-8a2d-bcde8470d0b3") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("a01bb0d8-1cc6-4118-8a2d-bcde8470d0b3"), 0, "b4caf670-e916-43ac-9fb5-88de5c713250", new DateTime(1999, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "anh453138@gmail.com", true, "Anh", "Nguyen", false, null, "anh453138@gmail.com", "admin1", "AQAAAAEAACcQAAAAEFfbxLgpFhhSIzjUqec8Uq3hX6l0p0ngabQ40pMTWOUWOd0Nd7nXggX0GFeHhe+wOw==", null, false, "", false, "admin1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("83936c8b-1a07-4747-9f33-f5a07815267f"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("83936c8b-1a07-4747-9f33-f5a07815267f"), new Guid("a01bb0d8-1cc6-4118-8a2d-bcde8470d0b3") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("a01bb0d8-1cc6-4118-8a2d-bcde8470d0b3"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatedCreate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 8, 17, 42, 39, 890, DateTimeKind.Local).AddTicks(1064),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 25, 18, 15, 21, 994, DateTimeKind.Local).AddTicks(6135));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 8, 17, 42, 39, 899, DateTimeKind.Local).AddTicks(4142),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 25, 18, 15, 22, 11, DateTimeKind.Local).AddTicks(1026));
        }
    }
}
