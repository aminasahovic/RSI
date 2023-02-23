using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class dodajMaticnu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaticnaKnjiga",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentid = table.Column<int>(type: "int", nullable: true),
                    datumUpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    godinaStudija = table.Column<int>(type: "int", nullable: false),
                    akademskaGodinaid = table.Column<int>(type: "int", nullable: true),
                    cijena = table.Column<float>(type: "real", nullable: false),
                    datumOvjere = table.Column<DateTime>(type: "datetime2", nullable: true),
                    obnova = table.Column<bool>(type: "bit", nullable: false),
                    napomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    evidentiraoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaticnaKnjiga", x => x.id);
                    table.ForeignKey(
                        name: "FK_MaticnaKnjiga_AkademskaGodina_akademskaGodinaid",
                        column: x => x.akademskaGodinaid,
                        principalTable: "AkademskaGodina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaticnaKnjiga_KorisnickiNalog_evidentiraoid",
                        column: x => x.evidentiraoid,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaticnaKnjiga_Student_studentid",
                        column: x => x.studentid,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaticnaKnjiga_akademskaGodinaid",
                table: "MaticnaKnjiga",
                column: "akademskaGodinaid");

            migrationBuilder.CreateIndex(
                name: "IX_MaticnaKnjiga_evidentiraoid",
                table: "MaticnaKnjiga",
                column: "evidentiraoid");

            migrationBuilder.CreateIndex(
                name: "IX_MaticnaKnjiga_studentid",
                table: "MaticnaKnjiga",
                column: "studentid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaticnaKnjiga");
        }
    }
}
