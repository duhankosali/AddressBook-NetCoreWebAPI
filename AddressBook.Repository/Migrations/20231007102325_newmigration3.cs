using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddressBook.Repository.Migrations
{
    public partial class newmigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_Phone",
                table: "Contacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Phone",
                table: "Contacts",
                column: "Phone",
                unique: true);
        }
    }
}
