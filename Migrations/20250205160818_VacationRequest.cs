using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management.Migrations
{
    /// <inheritdoc />
    public partial class VacationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VacationType",
                table: "VacationType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestState",
                table: "RequestState");

            migrationBuilder.RenameTable(
                name: "VacationType",
                newName: "VacationTypes");

            migrationBuilder.RenameTable(
                name: "RequestState",
                newName: "RequestStates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacationTypes",
                table: "VacationTypes",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestStates",
                table: "RequestStates",
                column: "StateId");

            migrationBuilder.CreateTable(
                name: "VacationRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(6)", nullable: false),
                    VacationType_Code = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Start_Date = table.Column<DateTime>(type: "date", nullable: false),
                    End_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Total_Days = table.Column<int>(type: "int", nullable: false),
                    RequestState_Id = table.Column<int>(type: "int", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(6)", nullable: true),
                    DeclinedBy = table.Column<string>(type: "nvarchar(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_VacationRequests_Employees_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeNumber");
                    table.ForeignKey(
                        name: "FK_VacationRequests_Employees_DeclinedBy",
                        column: x => x.DeclinedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeNumber");
                    table.ForeignKey(
                        name: "FK_VacationRequests_Employees_EmployeeNumber",
                        column: x => x.EmployeeNumber,
                        principalTable: "Employees",
                        principalColumn: "EmployeeNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacationRequests_RequestStates_RequestState_Id",
                        column: x => x.RequestState_Id,
                        principalTable: "RequestStates",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacationRequests_VacationTypes_VacationType_Code",
                        column: x => x.VacationType_Code,
                        principalTable: "VacationTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_ApprovedBy",
                table: "VacationRequests",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_DeclinedBy",
                table: "VacationRequests",
                column: "DeclinedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_EmployeeNumber",
                table: "VacationRequests",
                column: "EmployeeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_RequestState_Id",
                table: "VacationRequests",
                column: "RequestState_Id");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_VacationType_Code",
                table: "VacationRequests",
                column: "VacationType_Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacationRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacationTypes",
                table: "VacationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestStates",
                table: "RequestStates");

            migrationBuilder.RenameTable(
                name: "VacationTypes",
                newName: "VacationType");

            migrationBuilder.RenameTable(
                name: "RequestStates",
                newName: "RequestState");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacationType",
                table: "VacationType",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestState",
                table: "RequestState",
                column: "StateId");
        }
    }
}
