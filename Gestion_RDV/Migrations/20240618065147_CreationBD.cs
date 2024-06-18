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
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address",
                table: "t_e_user_usr");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address",
                table: "t_e_user_usr",
                column: "adr_id",
                principalTable: "t_e_adress_adr",
                principalColumn: "adr_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address",
                table: "t_e_user_usr");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address",
                table: "t_e_user_usr",
                column: "adr_id",
                principalTable: "t_e_adress_adr",
                principalColumn: "adr_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
