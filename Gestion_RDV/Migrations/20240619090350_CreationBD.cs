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
            migrationBuilder.DropColumn(
                name: "ofc_nb_no",
                table: "t_e_office_ofc");

            migrationBuilder.DropColumn(
                name: "ofc_nb_yes",
                table: "t_e_office_ofc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ofc_nb_no",
                table: "t_e_office_ofc",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ofc_nb_yes",
                table: "t_e_office_ofc",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
