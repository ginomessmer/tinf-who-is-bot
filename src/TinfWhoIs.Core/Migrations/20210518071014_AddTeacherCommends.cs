using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TinfWhoIs.Core.Migrations
{
    public partial class AddTeacherCommends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherCommend",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthoredBy = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    TeacherId = table.Column<int>(type: "integer", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCommend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherCommend_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "Key",
                value: "Qualit�t");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Key" },
                values: new object[] { "Werden gute �bungsaufgaben bereitgestellt?", "�bungsaufgaben" });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Wirkt die Person an andere Projekte oder sonstige Aktivit�ten mit?");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Ist die Person offen f�r Feedback und Vorschl�ge? Werden Studenten aktiv miteinbezogen?");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Key" },
                values: new object[] { "Darunter z�hlen bspw. die Reaktionszeit, die Qualit�t der Antwort, etc.", "Zuverl�sslichkeit bei Emails" });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Sind Altklausuren vorhanden, die entweder von der Person oder durch �ltere Jahrg�nge bereitgestellt wurden?");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCommend_TeacherId",
                table: "TeacherCommend",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherCommend");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "Key",
                value: "Qualität");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Key" },
                values: new object[] { "Werden gute Übungsaufgaben bereitgestellt?", "Übungsaufgaben" });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Wirkt die Person an andere Projekte oder sonstige Aktivitäten mit?");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Ist die Person offen für Feedback und Vorschläge? Werden Studenten aktiv miteinbezogen?");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Key" },
                values: new object[] { "Darunter zählen bspw. die Reaktionszeit, die Qualität der Antwort, etc.", "Zuverlässlichkeit bei Emails" });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Sind Altklausuren vorhanden, die entweder von der Person oder durch ältere Jahrgänge bereitgestellt wurden?");
        }
    }
}
