using Microsoft.EntityFrameworkCore.Migrations;

namespace DAB_Assignment_2.Migrations
{
    public partial class MyDBContextdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nation",
                columns: table => new
                {
                    nationName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nation", x => x.nationName);
                });

            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    MunicipalityID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Population = table.Column<float>(type: "REAL", nullable: false),
                    nationName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.MunicipalityID);
                    table.ForeignKey(
                        name: "FK_Municipality_Nation_nationName",
                        column: x => x.nationName,
                        principalTable: "Nation",
                        principalColumn: "nationName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Citizen",
                columns: table => new
                {
                    SocialSecurityNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Sex = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    MunicipalityID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizen", x => x.SocialSecurityNumber);
                    table.ForeignKey(
                        name: "FK_Citizen_Municipality_MunicipalityID",
                        column: x => x.MunicipalityID,
                        principalTable: "Municipality",
                        principalColumn: "MunicipalityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    MunicipalityID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Address);
                    table.ForeignKey(
                        name: "FK_Location_Municipality_MunicipalityID",
                        column: x => x.MunicipalityID,
                        principalTable: "Municipality",
                        principalColumn: "MunicipalityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCenter",
                columns: table => new
                {
                    TestCenterID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hours = table.Column<string>(type: "TEXT", nullable: true),
                    MunicipalityID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCenter", x => x.TestCenterID);
                    table.ForeignKey(
                        name: "FK_TestCenter_Municipality_MunicipalityID",
                        column: x => x.MunicipalityID,
                        principalTable: "Municipality",
                        principalColumn: "MunicipalityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationCitizen",
                columns: table => new
                {
                    SocialSecurityNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCitizen", x => new { x.SocialSecurityNumber, x.Address });
                    table.ForeignKey(
                        name: "FK_LocationCitizen_Citizen_SocialSecurityNumber",
                        column: x => x.SocialSecurityNumber,
                        principalTable: "Citizen",
                        principalColumn: "SocialSecurityNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationCitizen_Location_Address",
                        column: x => x.Address,
                        principalTable: "Location",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCenterCitizen",
                columns: table => new
                {
                    SocialSecurityNumber = table.Column<string>(type: "TEXT", nullable: false),
                    TestCenterID = table.Column<int>(type: "INTEGER", nullable: false),
                    result = table.Column<bool>(type: "INTEGER", nullable: false),
                    status = table.Column<string>(type: "TEXT", nullable: true),
                    date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCenterCitizen", x => new { x.SocialSecurityNumber, x.TestCenterID });
                    table.ForeignKey(
                        name: "FK_TestCenterCitizen_Citizen_SocialSecurityNumber",
                        column: x => x.SocialSecurityNumber,
                        principalTable: "Citizen",
                        principalColumn: "SocialSecurityNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestCenterCitizen_TestCenter_TestCenterID",
                        column: x => x.TestCenterID,
                        principalTable: "TestCenter",
                        principalColumn: "TestCenterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCenterManagement",
                columns: table => new
                {
                    PhoneNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    TestCenterID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCenterManagement", x => x.PhoneNumber);
                    table.ForeignKey(
                        name: "FK_TestCenterManagement_TestCenter_TestCenterID",
                        column: x => x.TestCenterID,
                        principalTable: "TestCenter",
                        principalColumn: "TestCenterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citizen_MunicipalityID",
                table: "Citizen",
                column: "MunicipalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_MunicipalityID",
                table: "Location",
                column: "MunicipalityID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCitizen_Address",
                table: "LocationCitizen",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Municipality_nationName",
                table: "Municipality",
                column: "nationName");

            migrationBuilder.CreateIndex(
                name: "IX_TestCenter_MunicipalityID",
                table: "TestCenter",
                column: "MunicipalityID");

            migrationBuilder.CreateIndex(
                name: "IX_TestCenterCitizen_TestCenterID",
                table: "TestCenterCitizen",
                column: "TestCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_TestCenterManagement_TestCenterID",
                table: "TestCenterManagement",
                column: "TestCenterID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationCitizen");

            migrationBuilder.DropTable(
                name: "TestCenterCitizen");

            migrationBuilder.DropTable(
                name: "TestCenterManagement");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Citizen");

            migrationBuilder.DropTable(
                name: "TestCenter");

            migrationBuilder.DropTable(
                name: "Municipality");

            migrationBuilder.DropTable(
                name: "Nation");
        }
    }
}
