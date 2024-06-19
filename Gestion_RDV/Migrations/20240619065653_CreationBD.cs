using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gestion_RDV.Migrations
{
    /// <inheritdoc />
    public partial class CreationBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_address_adr",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    adr_adresse = table.Column<string>(type: "text", nullable: false),
                    adr_ville = table.Column<string>(type: "text", nullable: false),
                    adr_codepostal = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.adr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_conversation_cnv",
                columns: table => new
                {
                    cnv_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cnv_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.cnv_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_user_usr",
                columns: table => new
                {
                    usr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usr_first_name = table.Column<string>(type: "text", nullable: false),
                    usr_last_name = table.Column<string>(type: "text", nullable: false),
                    usr_email = table.Column<string>(type: "text", nullable: false),
                    usr_password = table.Column<string>(type: "text", nullable: false),
                    usr_birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    usr_activated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    usr_avatar = table.Column<string>(type: "text", nullable: true),
                    usr_secret_token = table.Column<string>(type: "text", nullable: true),
                    usr_role = table.Column<string>(type: "text", nullable: false),
                    usr_sexe = table.Column<string>(type: "text", nullable: true),
                    usr_telephone = table.Column<string>(type: "text", nullable: true),
                    adr_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.usr_id);
                    table.ForeignKey(
                        name: "FK_User_Address",
                        column: x => x.adr_id,
                        principalTable: "t_e_address_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "t_e_office_ofc",
                columns: table => new
                {
                    ofc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ofc_diplome = table.Column<string>(type: "text", nullable: false),
                    ofc_image_diplome = table.Column<string>(type: "text", nullable: false),
                    ofc_rating = table.Column<double>(type: "double precision", nullable: false),
                    ofc_domaine_principal = table.Column<string>(type: "text", nullable: false),
                    ofc_cv = table.Column<string>(type: "text", nullable: false),
                    ofc_description = table.Column<string>(type: "text", nullable: false),
                    ofc_metier = table.Column<string>(type: "text", nullable: false),
                    ofc_prix_pcr = table.Column<double>(type: "double precision", nullable: false),
                    ofc_video = table.Column<string>(type: "text", nullable: false),
                    ofc_nb_yes = table.Column<int>(type: "integer", nullable: false),
                    ofc_nb_no = table.Column<int>(type: "integer", nullable: false),
                    ofc_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ofc_telephone = table.Column<string>(type: "text", nullable: false),
                    usr_id = table.Column<int>(type: "integer", nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.ofc_id);
                    table.ForeignKey(
                        name: "FK_Office_Address",
                        column: x => x.adr_id,
                        principalTable: "t_e_address_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Office_User",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_post_pst",
                columns: table => new
                {
                    pst_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pst_text = table.Column<string>(type: "text", nullable: false),
                    pst_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    pst_type = table.Column<string>(type: "text", nullable: false, defaultValue: "text"),
                    usr_id = table.Column<int>(type: "integer", nullable: false),
                    p_pst_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_post_pst", x => x.pst_id);
                    table.ForeignKey(
                        name: "FK_t_e_post_pst_t_e_post_p_pst_id",
                        column: x => x.p_pst_id,
                        principalTable: "t_e_post_pst",
                        principalColumn: "pst_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_e_post_pst_t_e_user_usr_id",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_socialmediaaccount_sma",
                columns: table => new
                {
                    sma_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sma_platform = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    sma_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    usr_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaAccount", x => x.sma_id);
                    table.ForeignKey(
                        name: "FK_SocialMediaAccount_User",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_conversationuser_cvu",
                columns: table => new
                {
                    usr_id = table.Column<int>(type: "integer", nullable: false),
                    cnv_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationUser", x => new { x.usr_id, x.cnv_id });
                    table.ForeignKey(
                        name: "FK_ConversationUser_Conversation",
                        column: x => x.cnv_id,
                        principalTable: "t_e_conversation_cnv",
                        principalColumn: "cnv_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationUser_User",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_message_msg",
                columns: table => new
                {
                    msg_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    usr_id = table.Column<int>(type: "integer", nullable: false),
                    cnv_id = table.Column<int>(type: "integer", nullable: false),
                    msg_text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => new { x.usr_id, x.cnv_id, x.msg_created });
                    table.ForeignKey(
                        name: "FK_Message_Conversation",
                        column: x => x.cnv_id,
                        principalTable: "t_e_conversation_cnv",
                        principalColumn: "cnv_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_User",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_availability_avb",
                columns: table => new
                {
                    avb_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    avb_start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    avb_end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ofc_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availability", x => x.avb_id);
                    table.ForeignKey(
                        name: "FK_Availability_Office",
                        column: x => x.ofc_id,
                        principalTable: "t_e_office_ofc",
                        principalColumn: "ofc_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_rendezvous_rdv",
                columns: table => new
                {
                    rdv_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rdv_start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rdv_end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rdv_etat_id = table.Column<int>(type: "integer", nullable: false),
                    rdv_type_rendezvous = table.Column<string>(type: "text", nullable: false),
                    rdv_description = table.Column<string>(type: "text", nullable: false),
                    rdv_prix = table.Column<double>(type: "double precision", nullable: false),
                    rdv_id_event = table.Column<int>(type: "integer", nullable: false),
                    rdv_fichier_joint = table.Column<string>(type: "text", nullable: false),
                    usr_id = table.Column<int>(type: "integer", nullable: false),
                    ofc_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendezVous", x => x.rdv_id);
                    table.ForeignKey(
                        name: "FK_RendezVous_Office",
                        column: x => x.ofc_id,
                        principalTable: "t_e_office_ofc",
                        principalColumn: "ofc_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RendezVous_User",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_subscription_sub",
                columns: table => new
                {
                    usr_id = table.Column<int>(type: "integer", nullable: false),
                    ofc_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => new { x.usr_id, x.ofc_id });
                    table.ForeignKey(
                        name: "FK_Subscription_Office",
                        column: x => x.ofc_id,
                        principalTable: "t_e_office_ofc",
                        principalColumn: "ofc_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscription_User",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_like_post_lke",
                columns: table => new
                {
                    usr_id = table.Column<int>(type: "integer", nullable: false),
                    pst_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikePost", x => new { x.usr_id, x.pst_id });
                    table.ForeignKey(
                        name: "FK_LikePost_Post",
                        column: x => x.pst_id,
                        principalTable: "t_e_post_pst",
                        principalColumn: "pst_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikePost_User",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_devis_dvs",
                columns: table => new
                {
                    dvs_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dvs_professionelid = table.Column<int>(type: "integer", nullable: false),
                    dvs_patientid = table.Column<int>(type: "integer", nullable: false),
                    dvs_prix_avant_tva = table.Column<decimal>(type: "numeric", nullable: false),
                    dvs_tva = table.Column<decimal>(type: "numeric", nullable: false),
                    dvs_prix_final = table.Column<decimal>(type: "numeric", nullable: false),
                    rdv_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facture", x => x.dvs_id);
                    table.ForeignKey(
                        name: "FK_Facture_RendezVous",
                        column: x => x.rdv_id,
                        principalTable: "t_e_rendezvous_rdv",
                        principalColumn: "rdv_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_notification_ntf",
                columns: table => new
                {
                    ntf_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ntf_title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ntf_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    usr_id = table.Column<int>(type: "integer", nullable: false),
                    rdv_id = table.Column<int>(type: "integer", nullable: false),
                    ofc_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.ntf_id);
                    table.ForeignKey(
                        name: "FK_Notification_Office",
                        column: x => x.ofc_id,
                        principalTable: "t_e_office_ofc",
                        principalColumn: "ofc_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_RendezVous",
                        column: x => x.rdv_id,
                        principalTable: "t_e_rendezvous_rdv",
                        principalColumn: "rdv_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_User",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_review_rvw",
                columns: table => new
                {
                    rvw_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rvw_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    rvw_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rvw_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    rvw_note = table.Column<int>(type: "integer", nullable: false),
                    rdv_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.rvw_id);
                    table.ForeignKey(
                        name: "FK_Review_RendezVous",
                        column: x => x.rdv_id,
                        principalTable: "t_e_rendezvous_rdv",
                        principalColumn: "rdv_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_comment_cmt",
                columns: table => new
                {
                    cmt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cmt_text = table.Column<string>(type: "text", nullable: false),
                    cmt_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    usr_id = table.Column<int>(type: "integer", nullable: false),
                    rvw_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.cmt_id);
                    table.ForeignKey(
                        name: "FK_Comment_Review",
                        column: x => x.rvw_id,
                        principalTable: "t_e_review_rvw",
                        principalColumn: "rvw_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_User",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_like_review_lke",
                columns: table => new
                {
                    usr_id = table.Column<int>(type: "integer", nullable: false),
                    rvw_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeReview", x => new { x.usr_id, x.rvw_id });
                    table.ForeignKey(
                        name: "FK_LikeReview_Review",
                        column: x => x.rvw_id,
                        principalTable: "t_e_review_rvw",
                        principalColumn: "rvw_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeReview_User",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_availability_avb_ofc_id",
                table: "t_e_availability_avb",
                column: "ofc_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_comment_cmt_rvw_id",
                table: "t_e_comment_cmt",
                column: "rvw_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_comment_cmt_usr_id",
                table: "t_e_comment_cmt",
                column: "usr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_devis_dvs_rdv_id",
                table: "t_e_devis_dvs",
                column: "rdv_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_notification_ntf_ofc_id",
                table: "t_e_notification_ntf",
                column: "ofc_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_notification_ntf_rdv_id",
                table: "t_e_notification_ntf",
                column: "rdv_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_notification_ntf_usr_id",
                table: "t_e_notification_ntf",
                column: "usr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_office_ofc_adr_id",
                table: "t_e_office_ofc",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_office_ofc_usr_id",
                table: "t_e_office_ofc",
                column: "usr_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_post_pst_p_pst_id",
                table: "t_e_post_pst",
                column: "p_pst_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_post_pst_usr_id",
                table: "t_e_post_pst",
                column: "usr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_rendezvous_rdv_ofc_id",
                table: "t_e_rendezvous_rdv",
                column: "ofc_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_rendezvous_rdv_usr_id",
                table: "t_e_rendezvous_rdv",
                column: "usr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_review_rvw_rdv_id",
                table: "t_e_review_rvw",
                column: "rdv_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_socialmediaaccount_sma_usr_id",
                table: "t_e_socialmediaaccount_sma",
                column: "usr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_user_usr_adr_id",
                table: "t_e_user_usr",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_conversationuser_cvu_cnv_id",
                table: "t_j_conversationuser_cvu",
                column: "cnv_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_like_post_lke_pst_id",
                table: "t_j_like_post_lke",
                column: "pst_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_like_review_lke_rvw_id",
                table: "t_j_like_review_lke",
                column: "rvw_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_message_msg_cnv_id",
                table: "t_j_message_msg",
                column: "cnv_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_subscription_sub_ofc_id",
                table: "t_j_subscription_sub",
                column: "ofc_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_availability_avb");

            migrationBuilder.DropTable(
                name: "t_e_comment_cmt");

            migrationBuilder.DropTable(
                name: "t_e_devis_dvs");

            migrationBuilder.DropTable(
                name: "t_e_notification_ntf");

            migrationBuilder.DropTable(
                name: "t_e_socialmediaaccount_sma");

            migrationBuilder.DropTable(
                name: "t_j_conversationuser_cvu");

            migrationBuilder.DropTable(
                name: "t_j_like_post_lke");

            migrationBuilder.DropTable(
                name: "t_j_like_review_lke");

            migrationBuilder.DropTable(
                name: "t_j_message_msg");

            migrationBuilder.DropTable(
                name: "t_j_subscription_sub");

            migrationBuilder.DropTable(
                name: "t_e_post_pst");

            migrationBuilder.DropTable(
                name: "t_e_review_rvw");

            migrationBuilder.DropTable(
                name: "t_e_conversation_cnv");

            migrationBuilder.DropTable(
                name: "t_e_rendezvous_rdv");

            migrationBuilder.DropTable(
                name: "t_e_office_ofc");

            migrationBuilder.DropTable(
                name: "t_e_user_usr");

            migrationBuilder.DropTable(
                name: "t_e_address_adr");
        }
    }
}
