using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78ce9a43-39e9-49c8-9db3-7290e9a85e6f", "2", "User", "User" },
                    { "7db42a2e-ea55-4cfd-8c3d-9a6bee520c6e", "1", "Admin", "Admin" },
                    { "bbcc3d01-9595-4577-a19d-7d22c835b9d4", "3", "Manager", "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78ce9a43-39e9-49c8-9db3-7290e9a85e6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7db42a2e-ea55-4cfd-8c3d-9a6bee520c6e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbcc3d01-9595-4577-a19d-7d22c835b9d4");
        }
    }
}
