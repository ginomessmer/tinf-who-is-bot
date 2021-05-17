using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LecturerLookup.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Office = table.Column<string>(type: "text", nullable: true),
                    Telephone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseTeacher",
                columns: table => new
                {
                    CoursesId = table.Column<string>(type: "text", nullable: false),
                    TeachersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTeacher", x => new { x.CoursesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_CourseTeacher_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTeacher_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: true),
                    TeacherId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Evaluation_CalculatedScore = table.Column<int>(type: "integer", nullable: true),
                    Evaluation_TotalVotes = table.Column<int>(type: "integer", nullable: true),
                    Evaluation_TotalUpVotes = table.Column<int>(type: "integer", nullable: true),
                    Evaluation_TotalDownVotes = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherTag_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherTagVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherTagId = table.Column<Guid>(type: "uuid", nullable: false),
                    VoterId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    MessageId = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherTagVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherTagVotes_TeacherTag_TeacherTagId",
                        column: x => x.TeacherTagId,
                        principalTable: "TeacherTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "analysis", "Mathematik - Analysis" },
                    { "we2", "Web Engineering II" },
                    { "we1", "Web Engineering I" },
                    { "cb", "Compilerbau" },
                    { "fs", "Formale Sprachen" },
                    { "ti2", "Theoretische Informatik II" },
                    { "ti1", "Theoretische Informatik I" },
                    { "se", "Software Engineering" },
                    { "ase", "Advanced Software Engineering" },
                    { "marketing", "Marketing" },
                    { "bwl", "BWL" },
                    { "pm", "Projektmanagement" },
                    { "ic", "Intercultural Communication" },
                    { "statistik", "Mathematik - Statistik" },
                    { "numerik", "Mathematik - Numerik" },
                    { "algebra", "Mathematik - Lineare Algebra" },
                    { "prog", "Programmieren" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Description", "Key" },
                values: new object[,]
                {
                    { 7, "Darunter zählen bspw. die Reaktionszeit, die Qualität der Antwort, etc.", "Zuverlässlichkeit bei Emails" },
                    { 10, "Wie schnell werden Klausuren korrigiert?", "Klausurkorrektur" },
                    { 8, "Sind Altklausuren vorhanden, die entweder von der Person oder durch ältere Jahrgänge bereitgestellt wurden?", "Altklausuren" },
                    { 6, "Ist die Person offen für Feedback und Vorschläge? Werden Studenten aktiv miteinbezogen?", "Offenheit" },
                    { 9, "Ist das Niveau der Klausuren angemessen?", "Klausuren" },
                    { 4, "Finden ausreichende Wiederholungen des Inhalts statt?", "Wiederholungen" },
                    { 3, "Werden gute Übungsaufgaben bereitgestellt?", "Übungsaufgaben" },
                    { 2, "Wie ist das Tempo der Vorlesung?", "Tempo" },
                    { 1, "Wie ist die Organisation der Materialien und die Struktur der Vorlesung?", "Qualität" },
                    { 5, "Wirkt die Person an andere Projekte oder sonstige Aktivitäten mit?", "Engagiert" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "AvatarUrl", "Email", "IsApproved", "Location", "Name", "Office", "Telephone" },
                values: new object[,]
                {
                    { 141, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Pohl-Philipp-Prof.jpg", "philipp.pohl@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C558", "Pohl, Prof. Dr. Philipp", "Leiter Studiengang Wirtschaftsinformatik", "+49.721.9735-962" },
                    { 142, null, "schekeb.raoufi@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E004", "Raoufi,  Ahmed Schekeb", "Labor Mechatronik", "+49.721.9735-856" },
                    { 143, null, "steffen.rasch@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A596", "Rasch, Prof. Dr. Steffen", "Leiter Studiengang BWL - Bank", "+49.721.9735-918" },
                    { 144, null, "janmichael.rasimus@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A052", "Rasimus,  Jan Michael", "Eye Tracking-Labor - Fit For Digital Innovation", "+49.721.9735-865" },
                    { 145, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Ratz-Dietmar-Prof.jpg", "dietmar.ratz@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C566", "Ratz, Prof. Dr. Dietmar", "Leiter Studiengang Wirtschaftsinformatik", "+49.721.9735-954" },
                    { 146, null, "david.rausch@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C346", "Rausch,  David", "Labor Wirtschaftsinformatik", "+49.721.9735-932" },
                    { 147, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Reinhard-Stefan.jpg", "stefan.reinhard@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C132.0", "Reinhard,  Stefan", "Akademischer Mitarbeiter im Projekt ExGra", "+49.721.9735-863" },
                    { 148, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Reitze-Clemens-Prof.jpg", "clemens.reitze@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E506", "Reitze, Prof. Dr. Clemens", "Professor Fakultät Technik", "+49.721.9735-829" },
                    { 149, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Renschler-Theo.jpg", "theo.renschler@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D017", "Renschler,  Theo", "Hausdienst", "+49.721.9735-611 (Zentrale Nummer)" },
                    { 150, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Rettig-Oliver.jpg", "oliver.rettig@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D324", "Rettig, Dr. Oliver", "Projektmitarbeiter RaHM-Lab, ErgoBot", "+49.721.9735-622" },
                    { 151, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Richter-Anja.jpg", "anja.richter@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.9", "Richter,  Anja", "Mitarbeiterin ESC, E-Assessment im Studium", "+49.721.9735-835" },
                    { 154, null, "anke.roesch@dhbw-karlsruhe.de", true, "Erzbergerstraße 123/119, Raum A587 u. F375", "Rösch,  Anke", "Sekretariat Studiengang BWL-International Business Angewandte Hebammenwissenschaft", "+49.721.9735-979 / +49.721.9735-844" },
                    { 153, null, "harald.ritzenthaler@dhbw-karlsruhe.de", true, "Erzberger Str. 121, Raum F487", "Ritzenthaler,  Harald", "Leiter IT.Service Center", "+49.721.9735-892" },
                    { 155, null, "esther.roesch@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.11", "Rösch, Prof. Dr. Esther", "Leiterin Studiengang Sicherheitswesen", "+49.721.9735-807" },
                    { 156, null, "petra.rothfuss@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F382", "Rothfuß,  Petra", "Prüfungsamt", "+49.721.9735-736" },
                    { 157, null, "juergen.roethig@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C560", "Röthig, Prof. Dr. Jürgen", "Professor Fakultät Technik", "+49.721.9735-883" },
                    { 158, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Sandal-Cueneyt.jpg", "cueneyt.sandal@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D117.2", "Sandal,  Cüneyt", "Leitung ESC E-Assessment im Studium, technologiegestütztes Lehren/Lernen", "+49.721.9735-618" },
                    { 159, null, "gunter.schaefer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C132.0", "Schäfer,  Gunter", "Modellfabrik", "+49.721.9735-863" },
                    { 160, null, "karin.schaefer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E513", "Schäfer, Prof. Dr. Karin", "Prodekanin Fakultät TechnikLeiterin Studiengang Maschinenbau", "+49.721.9735-968" },
                    { 161, null, "ruediger.schaefer@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A592", "Schäfer, Prof. Dr. Rüdiger", "Leiter Studiengang BWL - Handel", "+49.721.9735-906" },
                    { 162, null, "ekkehard.scheffler@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F374", "Scheffler, Dr. Ekkehard", "Professurvertretung im Fachbereich Gesundheit", "+49.721.9735-871" },
                    { 163, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Schenkel-Stephan-Prof.jpg", "stephan.schenkel@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D535", "Schenkel, Prof. Dr.-Ing. Stephan", "Rektor", "+49.721.9735-700" },
                    { 140, null, "heike.pluemer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D534", "Plümer,  Heike", "Sekretariat Rektorat", "+49.721.9735-702" },
                    { 152, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Richter-Thomas.jpg", "thomas.richter@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D017", "Richter,  Thomas", "Hausdienst", "+49.721.9735-611 (Zentrale Nummer)" },
                    { 139, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Pickenhahn-Jens.jpg", "jens.pickenhahn@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F528.7", "Pickenhahn,  Jens", "Leiter Gebäudemanagement, Beschaffung, Sicherheit und Technik", "+49.721.9735-734" },
                    { 125, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Mueller-Holger.jpg", "holger.mueller@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D017", "Müller,  Holger", "Hausdienst", "+49.721.9735-611 (Zentrale Nummer)" },
                    { 137, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Pfannenschwarz-Armin-Prof.jpg", "armin.pfannenschwarz@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A164", "Pfannenschwarz, Prof. Dr. Armin", "Leiter Studiengang Unternehmertum", "+49.721.9735-953" },
                    { 113, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Lennerts-Silke-Prof.jpg", "silke.lennerts@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.8", "Lennerts, Prof. Dr. Silke", "Professor Fakultät Wirtschaft", "+49.721.9735-927" },
                    { 114, null, "george.lueth@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C346", "Lüth,  George", "Labor Wirtschaftsinformatik", "+49.721.9735-895" },
                    { 115, null, "joachim.lutz@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C131", "Lutz,  Joachim", "Laborleiter Maschinenbau", "+49.721.9735-860" },
                    { 116, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Lux-Kaufmann-Doreen.jpg", "doreen.lux-kaufmann@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A601", "Lux-Kaufmann,  Doreen", "Sekretariat Studiengang BWL - Digital Business Management", "+49.721.9735-907" },
                    { 117, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Magdanz_Nina.jpg", "nina.magdanz@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.12", "Magdanz,  Nina", "Projektmanagement optes", "+49.721.9735-612" },
                    { 118, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Mardian-Sabine.jpg", "sabine.mardian@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.1", "Mardian,  Sabine", "Hochschulkommunikation Social Media, Online-Redaktion, Veranstaltungen", "+49.721.9735-756" },
                    { 119, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Marschall-Tanja.jpg", "tanja.marschall@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F372", "Marschall,  Tanja", "Akademische Mitarbeiterin im Fachbereich Gesundheit", "+49.721.9735-825" },
                    { 120, null, "edith.mechelke-Schwede@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G080", "Mechelke-Schwede,  Edith", "Projektmitarbeiterin Online-Vorkurse Physik im AWZ", "+49.721.9735-629" },
                    { 121, null, "bettina.mend@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.2", "Mend,  Bettina", "Qualitätsmangement", "+49.721.9735-616" },
                    { 122, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Moebius-Christian-Prof.jpg", "christian.moebius@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.3", "Möbius, Prof. Dr. Christian", "Professor Fakultät Wirtschaft", "+49.721.9735-936" },
                    { 123, null, "tina.monelyon@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D322", "Monelyon,  Tina", "akademische Mitarbeiterin bei Herrn Prof. Ehlers", "+49.721.9735-647" },
                    { 138, null, "nadine.pflaumer@dhbw-karlsruhe.de", true, "Erzbergstraße 121", "Pflaumer,  Nadine", "Projektmitarbeiterin iREAD", "+49.721.9735-621" },
                    { 124, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Mueller-Margitte-Prof.jpg", "margitte.mueller@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A586", "Müller, Prof. Dr. Margitte", "Leiterin Studiengang International Business", "+49.721.9735-947" },
                    { 126, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Mueller-Silvan.jpg", "silvan.mueller@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D324", "Müller,  Silvan Michael", "Projektmitarbeiter InTransDig", "+49.721.9735-623" },
                    { 127, null, "martin.neubarth@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C130", "Neubarth,  Martin", "Laboringenieur Maschinenbau", "+49.721.9735-870" },
                    { 128, null, "quy.nguyen@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F 491", "Nguyen,  Quy", "Auszubildender zum Fachinformatiker der Fachrichtung Systemintegration", "+49.721.9735-847" },
                    { 129, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Nick-Albrecht-Prof.jpg", "albrecht.nick@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E508", "Nick, Prof. Dr. Albrecht", "Leiter Studiengang Maschinenbau Wissenschaftlicher Leiter International Office", "+49.721.9735-810" },
                    { 130, null, "thalea.nold@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F380", "Nold,  Thalea", "Prüfungsamt Hochschulzugang für beruflich Qualifizierte (Studienbereich Wirtschaft)", "+49.721.9735-741" },
                    { 131, null, "axel.oberacker@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F485", "Oberacker,  Axel", "IT Service Center", "+49.721.9735-897" },
                    { 132, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Oberschmidt-Gerald-Prof.jpg", "gerald.oberschmidt@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E520", "Oberschmidt, Prof. Dr.-Ing. Gerald", "Professor Fakultät Technik Fachliche Beratung Master Elektrotechnik und Integrated Engineering Wissenschaftliche Leitung Master Elektrotechnik", "+49.721.9735-886" },
                    { 133, null, "pahl@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C344", "Pahl,  M.", "Laborleiter Wirtschaftinformatik", "+49.721.9735-922" },
                    { 134, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Paulsen-Pay-Uwe-Prof.jpg", "pay-uwe.paulsen@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.7", "Paulsen, Prof. Dr. Pay-Uwe", "Professor Fakultät Wirtschaft", "+49.721.9735-934" },
                    { 135, null, "florian.pennington@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E406", "Pennington,  Florian", "Labor Elektrotechnik", "+49.721.9735-873" },
                    { 136, null, "marc.peterfi@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.14", "Peterfi,  Marc", "Projektmitarbeiter optes, eAssessment im Studium", "+49.721.9735-607" },
                    { 164, null, "christine.schiffmacher@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D015", "Schiffmacher,  Christine", "Information, Poststelle, Telefonzentrale", "+49.721.9735-601" },
                    { 165, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Schindler-Darius-Prof.jpg", "darius.schindler@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.6", "Schindler, Prof. Dr. Darius", "Professor Fakultät WirtschaftJustiziar", "+49.721.9735-980" },
                    { 179, null, "wolfgang.schwarz@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C443", "Schwarz,  Wolfgang", "Laboringenieur Sicherheitswesen", "+49.721.9735-834" },
                    { 167, null, "daniela.schmid@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A595", "Schmid,  Daniela", "Sekretariat BWL - Bank", "+49.721.9735-902" },
                    { 196, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Vollmer-Juergen-Prof.jpg", "juergen.vollmer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C557", "Vollmer, Prof. Dr. Jürgen", "Leiter Studiengang Informatik", "+49.721.9735-814" },
                    { 197, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/von-Stehlik-Jeanine-Prof.jpg", "jeanine.vonstehlik@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A162", "von Stehlik, Prof. Dr. iur. Jeanine", "Leiterin Studiengang Unternehmertum", "Telefon +49.721.9735-986" },
                    { 198, null, "regina.vonwagner-heep@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F485", "von Wagner-Heep,  Regina", "IT Service Center", "+49.721.9735-970" },
                    { 199, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Wallrath-Mechtild-Prof.jpg", "mechtild.wallrath@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C562", "Wallrath, Prof. Dr. Mechtild", "Prodekanin Fakultät WirtschaftLeiterin Studiengang Wirtschaftsinformatik", "+49.721.9735-942" },
                    { 200, null, null, true, "Erzbergerstraße 121, Raum D322", "Walter,  Lukas", "als akademischer Mitarbeiter im Projekt PolyProg bei Herrn Prof. Kauffmann", null },
                    { 201, null, null, true, "Scheffelstraße 29, 76593 Gernsbach", "Walz,  Ulla", "Projektmitarbeiterin ACC-Verpackung", "ulla.walz@dhbw-karlsruhe.de" },
                    { 202, null, "andreas.weber@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C547", "Weber, Prof. Dr. Andreas", "Professor Fakultät Wirtschaft", "+49.721.9735-931" },
                    { 203, null, "heidi.weber@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C550", "Weber,  Heidi", "Sekretariat Studiengang BWL - IndustrieSekretariat Studiengang Wirtschaftsinformatik", "+49.721.9735-961" },
                    { 204, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Weber-Marco.jpg", "marco.weber@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A606", "Weber,  Marco", "Studiengangsmanager WirtschaftsinformatikStudiengangsmanager ZfwU", "+49.721.9735-634" },
                    { 205, null, "harald.wehner@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.6", "Wehner,  Harald", "Laboringenieur für Kunststofftechnik", "+49.721.9735-837" },
                    { 206, null, "xin.wei@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.14", "Wei,  Xin", "Projektmitarbeiterin optes, eAssessment im Studium", "Telefon +49.721.9735-624" },
                    { 195, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Vogel-Stefanie.jpg", "stefanie.vogel@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D426", "Vogel,  Stefanie", "Multimedia Lernzentrum / Sprachenzentrum, Projekt FestBW", "+49.721.9735-627" },
                    { 207, null, "dominik.weickgenannt@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E512", "Weickgenannt,  Dominik", "Laborleiter Smart Factory Labs", "+49.721.9735-822" },
                    { 209, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Weinmann-Martin-Prof.jpg", "martin.weinmann@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A605", "Weinmann, Prof. Dr. Martin", "Leiter Studiengang BWL - Industrie Leiter Studiengang BWL - Digital Business Management", "+49.721.9735-915" },
                    { 210, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Weissenborn-Marina.jpg", "marina.weissenborn@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D118.1", "Weißenborn,  Marina", "International Office", "+49.721.9735-729" },
                    { 211, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Wengler-Katja-Prof.jpg", "katja.wengler@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C552", "Wengler, Prof. Dr. Katja", "Leiterin Studiengang Wirtschaftsinformatik", "+49.721.9735-909" },
                    { 212, null, "johanna.wichert@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D530", "Wichert,  Johanna", "Aushilfe im Personalservice", "+49.721.9735-712" },
                    { 213, null, "stefanie.wild@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E512", "Wild,  Stefanie", "Sekretariat Studiengang Maschinenbau", "+49.721.9735-826" },
                    { 214, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Wilkesmann-Susanne.jpg", "susanne.wilkesmann@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D530", "Wilkesmann,  Susanne", "Personalservice - Beschäftigte Reisestelle", "+49.721.9735-712" },
                    { 215, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Winheim-Susanne.jpg", "susanne.winheim@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A591", "Winheim,  Susanne", "Sekretariat Studiengang BWL - Handel", "+49.721.9735-982" },
                    { 216, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Witte-Sarah.jpg", "sarah.witte@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D118.1", "Witte,  Sarah", "Leiterin International Office", "+49.721.9735-709" },
                    { 217, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Wolf-David.jpg", "david.wolf@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D531", "Wolf,  David", "Verwaltungsdirektor", "+49.721.9735-710" },
                    { 218, null, "barbara.wolf@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G090", "Wolf,  Barbara", "Anwendungszentrum E-Learning", "+49.721.9735-869" },
                    { 219, null, "iris.wuttke-hilke@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A064", "Wuttke-Hilke,  Iris", "Akademische Mitarbeiterin im ZfwU", "+49.721.9735-983" },
                    { 208, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Weiland-Christiane-Prof.jpg", "christiane.weiland@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A594", "Weiland, Prof. Dr. Christiane", "Leiterin Studiengang BWL - Bank", "+49.721.9735-903" },
                    { 166, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Schlenker-Birgit.jpg", "birgit.schlenker@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.0", "Schlenker,  Birgit", "Allgemeine Studienberatung Hochschulkommunikation International Office", "+49.721.9735-976" },
                    { 194, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Valkama-Jukka-Prof.jpg", "jukka-pekka.valkama@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E509", "Valkama, Prof. Dr. Jukka-Pekka", "Leiter Studiengang Papiertechnik", "+49.721.9735-839" },
                    { 192, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Tottermann-Rita.jpg", "rita.tottermann@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D530", "Tottermann,  Rita", "Sekretariat des Verwaltungsdirektors Zahlstelle und Führung des Dienstsiegels Personalservice - Beschäftigte", "+49.721.9735-711" },
                    { 168, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Schmidt-Michael.jpg", "michael.schmidt@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C346", "Schmidt,  Michael", "Labor Wirtschaftsinformatik", "+49.721.9735-926" },
                    { 169, null, "christine.schmitt@dhbw-karlsruhe.de", true, "Erzbergerstraße 121 u. 119, Raum F382 u. G185.1", "Schmitt,  Christine", "Prüfungsamt Sekretariat Studiengang Sicherheitswesen", "+49.721.9735-731" },
                    { 170, null, "wibke.schmitt@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C132.0", "Schmitt,  Wibke", "akademische Mitarbeiterin im Projekt BioPCM", "+49.721.9735-859" },
                    { 171, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Schnaubelt-Manuela.jpg", "manuela.schnaubelt@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D118.1", "Schnaubelt,  Manuela", "International Office", "+49.721.9735-757" },
                    { 172, null, "michael.schneider@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E312", "Schneider,  Michael", "Laborleiter Informatik", "+49.721.9735-849" },
                    { 173, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Schneider-Ulrike.jpg", "ulrike.schneider@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A064", "Schneider,  Ulrike", "Sekretariat Studiengang BWL-Deutsch-Französisches Management Projektmanagement EU-CAB", "+49.721.9735-664" },
                    { 174, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Ohngemach-Christina.jpg", "christina.schneider@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D117.2", "Schneider,  Christina", "Mitarbeiterin ESC, Studienvorbereitung, Projekt teach@DHBW, Projekt Z", "+49.721.9735-642" },
                    { 175, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Schorr-Dietmar-Prof.jpg", "dietmar.schorr@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E510", "Schorr, Prof. Dr. Dietmar", "Leiter Studiengang MaschinenbauWissenschaftliche Leitung Master MaschinenbauFachliche Beratung Master Maschinenbau und Wirtschaftsingenieurwesen", "+49.721.9735-831" },
                    { 176, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Schreiber-Anne.jpg", "anne.schreiber@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D117.2", "Schreiber, Dr. Anne", "Leitung ESC Bildungsforschung, Projekt- und Forschungsmanagement", "+49.721.9735-939" },
                    { 177, null, "florian.schwaer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C132.0", "Schwär,  Florian", "wissenschaftlicher Mitarbeiter bei Herrn Prof. Kauffmann", "+49.721.9735-646" },
                    { 178, null, "kerstin.schwarz@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F377", "Schwarz,  Kerstin", "Sekretariat Studiengang Arztassistent", "+49.721.9735-872" },
                    { 193, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Toussaint-Christine.jpg", "christine.toussaint@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.3", "Toussaint,  Christine", "Studiengangsmanagerin Wirtschaftsinformatik", "+49.721.9735-898" },
                    { 112, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Leibhammer-Tanja.jpg", "tanja.leibhammer@dhbw-karlsruhe.de", true, "Erzberger Str. 123, Raum A591", "Leibhammer,  Tanja", "Sekretariat Studiengang BWL-Handel", "+49.721.9735-923" },
                    { 181, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Senghas-Monika.jpg", "monika.senghas@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A585 u. A601", "Senghas,  Monika", "Sekretariat Studiengang BWL - Industrie-IDM Sekretariat Studiengang BWL - Versicherung", "+49.721.9735-913 / +49.721.9735-908" },
                    { 182, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Siedle-Johannes.jpg", "johannes.siedle@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F489", "Siedle,  Johannes", "IT Service Center", "+49.721.9735-896" },
                    { 183, null, "marion.smith@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C548", "Smith,  Marion", "Sekretariat Studiengang Informatik", "+49.721.9735-808" },
                    { 184, null, "birgit.speck@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C550", "Speck,  Birgit", "Sekretariat Studiengang Wirtschaftsinformatik", "+49.721.9735-941" },
                    { 185, null, "thomas.speck@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F483", "Speck,  Thomas", "Personalrat", "+49.721.9735-890" },
                    { 186, null, "gundula.stoltz@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C564", "Stoltz,  Gundula", "Sekretariat Studiengang Wirtschaftsinformatik", "+49.721.9735-981" },
                    { 187, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Strand-Marcus-Prof.jpg", "marcus.strand@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C551", "Strand, Prof. Dr. Marcus", "Leiter Studiengang Informatik", "+49.721.9735-924" },
                    { 188, null, "sabine.stroh@dhbw-karlsruhe.de", true, "Scheffelstraße 29, 76593 Gernsbach u. Erzbergerstraße 121, Raum E519", "Stroh,  Sabine", "Projektmitarbeiterin ACC-Verpackung Vertretung Sekretariat Papiertechnik", "+49.721.9735-813" },
                    { 189, null, "gabriele.thuesing@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.2", "Thüsing,  Gabriele", "Sekretariat Studiengang Wirtschaftsingenieurwesen", "+49.721.9735-817" },
                    { 190, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Tittelbach-Helmrich-Dietlind-Prof.jpg", "dietlind.tittelbach-helmrich@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F373", "Tittelbach-Helmrich, Prof. Dr. Dietlind", "Leiterin Studiengang Arztassistent", "+49.721.9735-853" },
                    { 191, null, "anneliese.tometten-iseke@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F378", "Tometten-Iseke, Prof. Dr. Anneliese", "Professorin im Studiengang Angewandte Hebammenwissenschaft", "+49.721.9735-840" },
                    { 180, null, "stephanie.schwarz@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.6", "Schwarz,  Stephanie", "Projektleiterin INTEGRA", "+49.721.9735-615" },
                    { 111, null, "peter.lehmeier@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A588", "Lehmeier, Prof. Peter", "Leiter Studiengang BWL - HandelFachliche Beratung Master Fachbereich Wirtschaft", "+49.721.9735-904/ -905" },
                    { 97, null, "jan.kopka@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F 528.9", "Kopka,  Jan", "Haushaltsangelegenheiten", "+49.721.9735-724" },
                    { 109, null, "evelyn.lautenschlager@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F 372", "Lautenschlager,  Evelyn", "Akademische Mitarbeiterin im Fachbereich Gesundheit", "+49.721.9735-882" },
                    { 29, null, "marina.brunner@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D322", "Brunner,  Marina", "akademische Mitarbeiterin im Bereich Lerntechnologien und Digitalisierung", "+49.721.9735-641" },
                    { 30, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Buske-Antje-Prof.jpg", null, true, "Erzbergerstraße 121, Raum B570.5", "Buske, Prof. Dr. Antje", "Professorin Fakultät Wirtschaft", "+49.721.9735-975" },
                    { 31, null, "martha.cabrera@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C559", "Cabrera,  Martha", "Sekretariat Informatik", "+49.721.9735-806" },
                    { 32, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Cappel-Anne.jpg", "anne.cappel@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D533", "Cappel,  Anne", "Hochschulkommunikation  Veranstaltungen, Messen, Schülerkommunikation", "+49.721.9735-704" },
                    { 33, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Daniel-Manfred-Prof.jpg", "manfred.daniel@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.9", "Daniel, Prof. Manfred", "Professor Wirtschaftsinformatik Teilprojektleiter optes, eAssessment im Studium", "+49.721.9735-938" },
                    { 34, null, "bernd.dannenmayer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.6", "Dannenmayer, Prof. Bernd", "Professor Fakultät Wirtschaft", "+49.721.9735-920" },
                    { 35, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Daum-Ina.jpg", "ina.daum@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F377", "Daum,  Ina", "Sekretariat Studiengang Angewandte Gesundheits- und Pflegewissenschaften", "+49.721.9735-874" },
                    { 36, null, "jasmin.davis@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C564", "Davis,  Jasmin", "Sekretariat Wirtschaftsinformatik", "+49.721.9735-944" },
                    { 37, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Detzel-Martin-Prof.jpg", "martin.detzel@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A597", "Detzel, Prof. Dr. Martin", "Leiter Studiengang BWL- Industrie Leiter Studiengang BWL-Digital Business Management", "+49.721.9735-916" },
                    { 38, null, "stephan.deyerler@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F528.3", "Deyerler,  Stephan", "Leitung Personalservice", "+49.721.9735-715" },
                    { 39, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Diehl-Becker-Angela-Prof.jpg", "angela.diehl-becker@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A602", "Diehl-Becker, Prof. Dr. Angela", "Leiterin Studiengang BWL - Deutsch-Französisches Management Projektkoordination EU-CAB", "+49.721.9735-984" },
                    { 28, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Bruederlin-Anke.jpg", "anke.bruederlin@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A606", "Brüderlin,  Anke", "Masterstudium am Standort Karlsruhe, Beratung zum dualen Master Hochschulkommunikation,&nbsp;Ansprechpartnerin Alumni-Team Studiengangsmanagerin ZfwU", "+49.721.9735-635" },
                    { 40, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Diringer-Susanne.jpg", "susanne.diringer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D533", "Diringer,  Susanne", "Hochschulkommunikation Presse- und Öffentlichkeitsarbeit, Forschungskommunikation", "+49.721.9735-718" },
                    { 42, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Dovoda-Jeanine.jpg", "jeannine.dovoda@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F528.4", "Dovođa,  Jeannine", "Haushaltsangelegenheiten", "+49.721.9735-723" },
                    { 43, null, "christine.drayer@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G090", "Drayer,  Christine", "Anwendungszentrum E-Learning", "+49.721.9735-608" },
                    { 44, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Ebert-Klaus.jpg", "klaus.ebert@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D016", "Ebert,  Klaus", "Hausdienst", "+49.721.9735-611 (Zentrale Nummer)" },
                    { 45, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Ehlers-Ulf-Daniel-Prof.jpg", "ulf-daniel.ehlers@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D427", "Ehlers, Prof. Dr. Ulf-Daniel", "Professor Fakultät Wirtschaft", "+49.721.9735-966" },
                    { 46, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Eidam-Dirk-Prof.jpg", "dirk.eidam@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, G185.7", "Eidam, Prof. Dr. Dirk", "Leiter Studiengang Wirtschaftsingenieurwesen", "+49.721.9735-827" },
                    { 47, null, "laura.eigbrecht@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D324", "Eigbrecht,  Laura", "akademische Mitarbeiterin im Bereich „Lerntechnologien und Digitalisierung“", "+49.721.9735-960" },
                    { 48, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Eisenbiegler-Joern-Prof.jpg", "joern.eisenbiegler@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, C554", "Eisenbiegler, Prof. Dr. Jörn", "Leiter Studiengang Informatik", "+49.721.9735-855" },
                    { 49, null, "iris.enders@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F382", "Enders,  Iris", "Prüfungsamt", "+49.721.9735-743" },
                    { 50, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Erb-Juergen-Prof.jpg", "juergen.erb@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.5", "Erb, Prof. Dr. Jürgen", "Professor Fakultät Technik", "+49.721.9735-867" },
                    { 51, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Etter-Ute.jpg", "ute.etter@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D015", "Etter,  Ute", "Information, Poststelle, Telefonzentrale", "+49.721.9735-601" },
                    { 52, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Fassnacht-Angelika.jpg", "angelika.fassnacht@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A163", "Fassnacht,  Angelika", "Sekretariat Studiengang Unternehmertum", "+49.721.9735-973" },
                    { 41, null, "ralf.dorwarth@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E517", "Dorwarth, Prof. Dr. Ralf", "Leiter Studiengang Elektrotechnik", "+49.721.9735-802" },
                    { 53, null, "sabine.fischer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D532", "Fischer,  Sabine", "Leiterin Haushaltsangelegenheiten", "+49.721.9735-708" },
                    { 27, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Brockmans-Saartje-Prof.jpg", "sara.brockmans@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C549", "Brockmans, Prof. Dr. Sara", "Leiterin Studiengang Wirtschaftsinformatik", "+49.721.9735-988" },
                    { 25, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Braswell-Alexandra.jpg", "alexandra.braswell@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D118.1", "Braswell,  Alexandra", "International Office", "+49.721.9735-707" },
                    { 1, null, "britta.ahrens@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E516", "Ahrens,  Britta", "Sekretariat Studiengang Elektrotechnik", "+49.721.9735-804" },
                    { 2, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Ahrens-Ulrike.jpg", "ulrike.ahrens@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E516", "Ahrens,  Ulrike", "Sekretariat Studiengang Maschinenbau", "+49.721.9735-812" },
                    { 3, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Arheidt-Patrick.jpg", "patrick.arheidt@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F528.7", "Arheidt,  Patrick", "Gebäudemanagement, Beschaffung, Sicherheit und Technik", "+49.721.9735-714" },
                    { 4, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Avella-Felice-Alfredo-Prof.jpg", "felice-alfredo.avella@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A599", "Avella, Prof. Dr. Felice-Alfredo", "Professor Fakultät Wirtschaft", "+49.721.9735-978" },
                    { 5, null, "sabine.baehr@dhbw-karlsruhe.de", true, "Erzbergerstraße 123; Raum A579 u. A595", "Bähr,  Sabine", "Sekretariat Fakultät WirtschaftSekretariat Studiengang RSW-Steuern und Prüfungswesen", "+49.721.9735-901 / +49.721.9735-911" },
                    { 6, null, "heike.barth@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.2", "Barth,  Heike", "Sekretariat Studiengang Wirtschaftsingenieurwesen", "+49.721.9735-841" },
                    { 7, null, "bruno.bartl@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C031", "Bartl,  Bruno", "Modellfabrik", "+49.721.9735-875" },
                    { 8, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Basli-Amira.jpg", "amira.basli@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D533", "Basli,  Amira", "HochschulkommunikationAssistenz", "+49.721.9735-737" },
                    { 9, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Bastron-Eugenia.jpg", "eugenia.bastron@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F528.4", "Bastron,  Eugenia", "Haushaltscontrolling", "+49.721.9735-739" },
                    { 10, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Bauer-Michael-Prof.jpg", "michael.bauer@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.13", "Bauer, Prof. Dr. Michael", "Leiter Studiengang Mechatronik", "+49.721.9735-811" },
                    { 11, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Bayerl-Margit.jpg", "margit.bayerl@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A595", "Bayerl,  Margit", "Sekretariat BWL - Bank", "+49.721.9735-935" },
                    { 26, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Braun-Heinrich-Prof.jpg", "heinrich.braun@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C563", "Braun, Prof. Dr. Heinrich", "Leiter Studiengang Informatik Fachliche Beratung Master Informatik Wissenschaftliche Leitung Master Informatik", "+49.721.9735-879" },
                    { 12, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Bearden-Jeremy.jpg", "jeremy.bearden@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D426", "Bearden,  Jeremy", "Mitarbeiter Multimedia Lernzentrum / Sprachenzentrum, am ESC im Projekt teach@DHBW", "+49.721.9735-648" },
                    { 14, null, "andrea.beetz@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F382", "Beetz,  Andrea", "Prüfungsamt Sekretariat Fakultät Wirtschaft", "+49.721.9735-738" },
                    { 15, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Bender-Oliver-Prof.jpg", "oliver.bender@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A583", "Bender, Prof. Dr. Oliver", "Leiter Studiengang BWL - VersicherungAlumni-Beauftragter", "+49.721.9735-917" },
                    { 16, null, "kay.berkling@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E511", "Berkling, Ph.D., Prof. Kay Margarethe", "Professorin Fakultät Technik", "+49.721.9735-864" },
                    { 17, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Boecker-Nicole.jpg", "nicole.boecker@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C548", "Böcker,  Nicole", "Sekretariat Studiengang Informatik lokale Prozessmanagerin", "+49.721.9735-815" },
                    { 18, null, "andrea.boden@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F528.6", "Boden,  Andrea", "Abrechnung Lehrvergütungen", "+49.721.9735-726" },
                    { 19, null, "dirk.boehm@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, A593", "Böhm, Prof. Dr. Dirk", "Leiter Studiengang BWL - Handel", "+49.721.9735-928" },
                    { 20, null, "silke.bohner@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G080", "Bohner,  Silke", "Teamassistenz AWZ", "+49.721.9735-682" },
                    { 21, null, "silke.bolze@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D534", "Bolze,  Silke", "Sekretariat Rektorat", "+49.721.9735-702" },
                    { 22, null, "patricia.bonaudo@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D324", "Bonaudo,  Patricia", "akademische Mitarbeiterin im Bereich „Lerntechnologien und Digitalisierung“", "+49.721.9735-663" },
                    { 23, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Borowicz-Frank-Prof.jpg", "frank.borowicz@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A604", "Borowicz, Prof. Dr. Frank", "Leiter Studiengang BWL - Industrie Leiter Studiengang BWL - Digital Business Management", "+49.721.9735-912" },
                    { 24, null, "christian.brandstetter@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A163", "Brandstetter,  Christian", "akademischer Mitarbeiter im Bereich Entrepreneurial Education", "+49.721.9735-958" },
                    { 13, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Becker-Holger-Prof.jpg", "holger.becker@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A580", "Becker, Prof. Dr. Holger", "ProrektorDekan Fakultät Wirtschaft", "+49.721.9735-900" },
                    { 110, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Lee-Andrew-Prof.jpg", "andrew.lee@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.7", "Lee, Prof. Dr. Andrew", "Professor Fakultät Wirtschaft", "+49.721.9735-974" },
                    { 54, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Franken-Birgit-Prof.jpg", "birgit.franken@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.5", "Franken, Prof. Dr. Birgit", "Professorin Fakultät Wirtschaft", "+49.721.9735-943" },
                    { 56, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Freytag-Thomas-Prof.jpg", "thomas.freytag@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C556", "Freytag, Prof. Dr. Thomas", "Professor Fakultät Wirtschaft Wissenschaftlicher Leiter International Office", "+49.721.9735-937" },
                    { 85, null, "karsten.junge@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.4", "Junge, Prof. Dr. Karsten", "Mitglied des Senats der DHBW Leiter Studiengang BWL - Industrie", "+49.721.9735-952" },
                    { 86, null, "monika.kary@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.10", "Kary,  Monika", "Sekretariat Studiengang Mechatronik", "+49.721.9735-878" },
                    { 87, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Kassel-Martina.jpg", "martina.kassel@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A591 u. A601", "Kassel,  Martina", "Sekretariat Studiengang BWL - Handel Sekretariat Studiengang BWL - Industrie-IDM", "+49.721.9735-905 /+49.721.9735-914" },
                    { 88, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Kauffmann-Axel-Prof.jpg", "axel.kauffmann@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.8", "Kauffmann, Prof. Dr. Axel", "Leiter Studiengang Wirtschaftsingenieurwesen", "+49.721.9735-836" },
                    { 89, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Kautz-Agnes.jpg", "agnes.kautz@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A601", "Kautz,  Agnes", "Sekretariat Studiengang BWL - Industrie-IDM", "+49.721.9735-985" },
                    { 90, null, "michael.keller@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E515", "Keller, Prof. Dr. Michael", "Leiter Studiengang Elektrotechnik", "+49.721.9735-803" },
                    { 91, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Keppner-Dominik.jpg", "dominik.keppner@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E311", "Keppner,  Dominik", "Labor Informatik", "+49.721.9735-848" },
                    { 92, null, "eberhard.keyl@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F378", "Keyl, Dr. Eberhard", "Professurvertretung in der Fakultät Wirtschaft", "+49.721.9735-969" },
                    { 93, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Klink-Stefan-Prof.jpg", "stefan.klink@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C547", "Klink, Prof. Dr. Stefan", "Professor Fakultät WirtschaftFachliche Beratung zum Master Wirtschaftsinformatik", "+49.721.9735-951" },
                    { 94, null, "barbara.kloeppner@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F 528.7", "Klöppner,  Barbara", "Gebäudemanagement, Beschaffung, Sicherheit und Technik", "+49.721.9735-716" },
                    { 95, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Kolb-Stefan-Prof.jpg", "stefan.kolb@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.3", "Kolb, Prof. Dr. Stefan", "Professor Fakultät Wirtschaft Fachliche Beratung zu den Master-Studiengängen der Wirtschaft", "+49 (0)721/9735-959" },
                    { 84, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Jackisch-Ingo.jpg", "ingo.jackisch@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G081", "Jackisch,  Ingo", "Leiter Anwendungszentrum E-Learning", "+49.721.9735-680" },
                    { 96, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Koelle-Alexandra.jpg", "alexandra.koelle@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.12", "Kölle,  Alexandra", "optes Projektmanagement", "+49.721.9735-660" },
                    { 98, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Kopra-Schaefer-Monika-Prof.jpg", "monika.kopra-schaefer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E509", "Kopra-Schäfer, Prof. Dr. Monika", "Professorin Fakultät Technik", "+49.721.9735-838" },
                    { 99, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Korzilius-Michael.jpg", "michael.korzilius@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F489", "Korzilius,  Michael", "IT Service Center", "+49.721.9735-850" },
                    { 100, null, "nora.kozonek@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D322", "Kozonek,  Nora", "wissenschaftliche Mitarbeiterin bei Herrn Prof. Strand", "+49.721.9735-639" },
                    { 101, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Krueckels-Ulrike.jpg", "ulrike.krueckels@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G080", "Krückels,  Ulrike", "Anwendungszentrum E-Learning", "+49.721.9735-609" },
                    { 102, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Kuehn-Ralf-Prof.jpg", "ralf.kuehn@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E522", "Kühn, Prof. Dr.-Ing. Ralf", "Professor Fakultät Technik", "+49.721.9735-884" },
                    { 103, null, "sophia.kunz@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E519 u. C559", "Kunz,  Sophia", "Sekretariat der Fakultät Technik Sekretariat Informatik", "+49.721.9735-801 / +49.721.9735-816" },
                    { 104, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Kuestermann-Roland-Prof.jpg", "roland.kuestermann@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E521", "Küstermann, Prof. Dr. Roland", "ProrektorDekan Fakultät Technik", "+49.721.9735-800" },
                    { 105, null, "sabine.landwehr-zloch@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A599", "Landwehr-Zloch, Prof. Dr. Sabine", "Professorin Fakultät Wirtschaft", "+49.721.9735-919" },
                    { 106, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Lauer-Silvia-Prof.jpg", "silvia.lauer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F490", "Lauer, Prof. Dr. Silvia", "Professorin Fakultät TechnikLeiterin Multimedia Lernzentrum / Sprachenzentrum", "+49.721.9735-887" },
                    { 107, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Lauinger-Matthias.jpg", "matthias.lauinger@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F528.9", "Lauinger,  Matthias", "Drittmittelverwaltung, Forschungsprojekte", "+49.721.9735-728" },
                    { 108, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Lausen-Ralph-Prof.jpg", "ralph.lausen@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C553", "Lausen, Prof. Dr. Ralph", "Professor Fakultät Technik", "+49.721.9735-877" },
                    { 220, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Zimmerman-Eric-Prof.jpg", "eric.zimmerman@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E507", "Zimmerman, Prof. Dr. Eric", "Professor Fakultät Technik", "+49.721.9735-824" },
                    { 55, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Freudenmann-Johannes-Prof.jpg", "johannes.freudenmann@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C561", "Freudenmann, Prof. Dr. Johannes", "Leiter Studiengang InformatikBeauftragter für Datenschutz", "+49.721.9735-880" },
                    { 83, null, "albena.ivanova@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.12", "Ivanova,  Albena", "Mitarbeiterin im Projekt „OPTES+ Projektassistenz optes", "+49.721.9735-625" },
                    { 81, null, "silke.huber@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum B570.2", "Huber,  Silke", "Sekretariat Fakultät Wirtschaft", "+49.721.9735-964" },
                    { 57, null, "patrick.froehlich@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum C346", "Fröhlich,  Patrick", "Auszubildender zum Fachinformatiker", "+49.721.9735-925" },
                    { 58, null, "estella.fürstenberg@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F 528.5", "Fürstenberg,  Estella", "Auszubildende zur Kauffrau für Büromanagement", "+49.721.9735-733" },
                    { 59, null, "anna.geisler@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A061", "Geisler,  Anna", "Planspiellabor", "+49.721.9735-631" },
                    { 60, null, "valeria.giurgiu@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum A585", "Giurgiu,  Valeria", "Sekretariat Studiengang BWL-Versicherung", "+49.721.9735-948" },
                    { 61, null, "katja.goetzmann@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F528.6", "Götzmann,  Katja", "Abrechnung Lehrvergütungen", "+49.721.9735-725" },
                    { 62, null, "klaus.grimm@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A598", "Grimm, Prof. Klaus", "Leiter Studiengang RSW - Steuern und Prüfungswesen", "+49.721.9735-933" },
                    { 63, null, "nora.groeninger@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D534", "Gröninger,  Nora", "Referentin des Rektors Leiterin Hochschulkommunikation", "+49.721.9735-761" },
                    { 64, null, "markus.gruen@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D570.8", "Grün, Prof. Dr. Markus", "Professor Fakultät Wirtschaft", "+49.721.9735-945" },
                    { 65, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Haalboom-Thomas-Prof.jpg", "thomas.haalboom@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.16", "Haalboom, Prof. Dr. Thomas", "Leiter Studiengang Mechatronik&nbsp;", "+49.721.9735-889" },
                    { 66, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Haas-Martin-Prof.jpg", "martin.haas@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, G185.4", "Haas, Prof. Martin", "Professor Fakultät Technik", "+49.721.9735-971" },
                    { 67, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Haeberer-Timo.jpg", "timo.haeberer@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F528.5", "Häberer,  Timo", "Personalservice - Beschäftigte", "+49.721.9735-732" },
                    { 82, null, "enrico.hueneborg@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E314", "Hüneborg,  Enrico", "Labor Informatik", "+49.721.9735-893" },
                    { 68, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Hager-Sibylle.jpg", "sibylle.hager@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G185.1", "Hager,  Sibylle", "Sekretariat Studiengang Wirtschaftsingenieurwesen Sekretariat Studiengang Mechatronik", "+49.721.9735-818" },
                    { 70, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Harms-Torsten-Prof.jpg", "torsten.harms@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A584", "Harms, Prof. Dr. Torsten", "Professor Fakultät Wirtschaft", "+49.721.9735-949" },
                    { 71, null, "tanja.heim@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A587", "Heim,  Tanja", "Sekretariat Studiengang BWL - Handel", "+49.721.9735-929" },
                    { 72, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Heinemann-Sonja.jpg", "sonja.heinemann@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F375", "Heinemann,  Sonja", "Akademische Mitarbeiterin im Studiengang Angewandte Hebammenwissenschaft", "+49.721.9735-832" },
                    { 73, null, "aneta.heinz@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D 322", "Heinz,  Aneta", "Wissenschaftliche Mitarbeiterin im IPF-Projekt „Weiterbildungsstrategien in der IT-Branche“, Projekt Z", "+49.721.9735-669" },
                    { 74, null, "daniel.helfrich@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F 528.8", "Helfrich,  Daniel", "Sachbearbeiter im Bereich Finanzwesen / Controlling", "+49.721.9735-753" },
                    { 75, null, "susanne.heller@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F486", "Heller,  Susanne", "IT Service Center", "+49.721.9735-894" },
                    { 76, null, "bernhard.herold@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A590", "Herold, Prof. Dr. Bernhard", "Leiter Studiengang BWL - Handel", "+49.721.9735-950" },
                    { 77, null, "martina.herzog@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F528.5", "Herzog,  Martina", "Personalservice - Beschäftigte", "+49.721.9735-727" },
                    { 78, null, "lisa.hiltl@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum D529", "Hiltl,  Lisa", "Stellvertretende Verwaltungsdirektorin Justiziarin Leiterin Prüfungsamt", "+49.721.9735-760" },
                    { 79, null, "jana.hoffmann@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum F380", "Hoffmann,  Jana", "Prüfungsamt Hochschulzugang für beruflich Qualifizierte (Studienbereiche Technik und Gesundheit)", "+49.721.9735-717" },
                    { 80, "https://www.karlsruhe.dhbw.de//fileadmin/user_upload/images/content/Bilder-Adressverwaltung/Houssi-Dhouha.jpg", "dhouha.houssi@dhbw-karlsruhe.de", true, "Erzbergerstraße 119, Raum G090", "Houssi,  Dhouha", "Anwendungszentrum E-Learning", "+49.721.9735-606" },
                    { 69, null, "susanne.hantsch@dhbw-karlsruhe.de", true, "Erzbergerstraße 121, Raum E519", "Hantsch,  Susanne", "Sekretariat Fakultät Technik Sekretariat Studiengang Papiertechnik", "+49.721.9735-801" },
                    { 221, null, "ruth-caroline.zimmermann@dhbw-karlsruhe.de", true, "Erzbergerstraße 123, Raum A062", "Zimmermann, Prof. Dr. Ruth-Caroline", "Professor Fakultät Wirtschaft", "+49.721.9735-972" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeacher_TeachersId",
                table: "CourseTeacher",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherTag_TagId",
                table: "TeacherTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherTag_TeacherId",
                table: "TeacherTag",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherTagVotes_TeacherTagId_VoterId",
                table: "TeacherTagVotes",
                columns: new[] { "TeacherTagId", "VoterId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTeacher");

            migrationBuilder.DropTable(
                name: "TeacherTagVotes");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "TeacherTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
