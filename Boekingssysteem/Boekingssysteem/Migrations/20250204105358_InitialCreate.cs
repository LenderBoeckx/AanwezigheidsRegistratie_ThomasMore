using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boekingssysteem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TM_Boekingssysteem");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Functie",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    FunctieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functie", x => x.FunctieID);
                });

            migrationBuilder.CreateTable(
                name: "Persoon",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    Personeelnummer = table.Column<string>(maxLength: 8, nullable: false),
                    Naam = table.Column<string>(nullable: false),
                    Voornaam = table.Column<string>(nullable: false),
                    Admin = table.Column<bool>(nullable: false),
                    Aanwezig = table.Column<bool>(nullable: true),
                    Wachtwoord = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persoon", x => x.Personeelnummer);
                });

            migrationBuilder.CreateTable(
                name: "Richting",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    RichtingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Richting", x => x.RichtingID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Afwezigheid",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    AfwezigheidID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Personeelnummer = table.Column<string>(maxLength: 8, nullable: false),
                    Begindatum = table.Column<DateTime>(nullable: false),
                    Einddatum = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afwezigheid", x => x.AfwezigheidID);
                    table.ForeignKey(
                        name: "FK_Afwezigheid_Persoon_Personeelnummer",
                        column: x => x.Personeelnummer,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "Persoon",
                        principalColumn: "Personeelnummer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Naam = table.Column<string>(nullable: true),
                    Personeelnummer = table.Column<string>(nullable: true),
                    Voornaam = table.Column<string>(nullable: true),
                    PersoonPersoneelnummer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Persoon_PersoonPersoneelnummer",
                        column: x => x.PersoonPersoneelnummer,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "Persoon",
                        principalColumn: "Personeelnummer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersoonFunctie",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    Personeelnummer = table.Column<string>(maxLength: 8, nullable: false),
                    FunctieID = table.Column<int>(nullable: false),
                    PersoonFunctieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersoonFunctie", x => new { x.FunctieID, x.Personeelnummer });
                    table.ForeignKey(
                        name: "FK_PersoonFunctie_Functie_FunctieID",
                        column: x => x.FunctieID,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "Functie",
                        principalColumn: "FunctieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersoonFunctie_Persoon_Personeelnummer",
                        column: x => x.Personeelnummer,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "Persoon",
                        principalColumn: "Personeelnummer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersoonRichting",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    Personeelnummer = table.Column<string>(maxLength: 8, nullable: false),
                    RichtingID = table.Column<int>(nullable: false),
                    PersoonRichtingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersoonRichting", x => new { x.RichtingID, x.Personeelnummer });
                    table.ForeignKey(
                        name: "FK_PersoonRichting_Persoon_Personeelnummer",
                        column: x => x.Personeelnummer,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "Persoon",
                        principalColumn: "Personeelnummer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersoonRichting_Richting_RichtingID",
                        column: x => x.RichtingID,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "Richting",
                        principalColumn: "RichtingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "TM_Boekingssysteem",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "TM_Boekingssysteem",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Afwezigheid_Personeelnummer",
                schema: "TM_Boekingssysteem",
                table: "Afwezigheid",
                column: "Personeelnummer");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "TM_Boekingssysteem",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "TM_Boekingssysteem",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "TM_Boekingssysteem",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "TM_Boekingssysteem",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "TM_Boekingssysteem",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "TM_Boekingssysteem",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "TM_Boekingssysteem",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersoonPersoneelnummer",
                schema: "TM_Boekingssysteem",
                table: "AspNetUsers",
                column: "PersoonPersoneelnummer");

            migrationBuilder.CreateIndex(
                name: "IX_PersoonFunctie_Personeelnummer",
                schema: "TM_Boekingssysteem",
                table: "PersoonFunctie",
                column: "Personeelnummer");

            migrationBuilder.CreateIndex(
                name: "IX_PersoonRichting_Personeelnummer",
                schema: "TM_Boekingssysteem",
                table: "PersoonRichting",
                column: "Personeelnummer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Afwezigheid",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "PersoonFunctie",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "PersoonRichting",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "Functie",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "Richting",
                schema: "TM_Boekingssysteem");

            migrationBuilder.DropTable(
                name: "Persoon",
                schema: "TM_Boekingssysteem");
        }
    }
}
