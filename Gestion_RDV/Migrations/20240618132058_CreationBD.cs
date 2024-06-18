using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_RDV.Migrations
{
    /// <inheritdoc />
    public partial class CreationBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "t_e_adress_adr",
                newName: "t_e_address_adr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "t_e_address_adr",
                newName: "t_e_adress_adr");
        }
    }
}
