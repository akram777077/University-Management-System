using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enrollments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    enrollment_date = table.Column<DateTime>(type: "date", nullable: false),
                    graduation_date = table.Column<DateTime>(type: "date", nullable: true),
                    notes = table.Column<string>(type: "varchar", maxLength: 500, nullable: true),
                    student_id = table.Column<int>(type: "integer", nullable: false),
                    program_id = table.Column<int>(type: "integer", nullable: false),
                    service_application_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrollments", x => x.id);
                    table.ForeignKey(
                        name: "FK_enrollments_programs_program_id",
                        column: x => x.program_id,
                        principalTable: "programs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_enrollments_service_applications_service_application_id",
                        column: x => x.service_application_id,
                        principalTable: "service_applications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_enrollments_students_student_id",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

                // Seed data for enrollments
                migrationBuilder.InsertData(
                table: "enrollments",
                columns: new[] { "enrollment_date", "graduation_date", "notes", "student_id", "program_id", "service_application_id" },
                values: new object[,]
                {
                    // Current enrollments
                    {
                        DateTime.Parse("2023-09-15"), 
                        DateTime.Parse("2025-06-30"), 
                        "Honors student with full scholarship", 
                        1,  // Student ID
                        1,  // Computer Science program
                        1   // Service Application ID
                    },
                    {
                        DateTime.Parse("2023-09-15"), 
                        DateTime.Parse("2025-06-30"), 
                        "International student from Canada", 
                        2,  // Student ID
                        2,  // Business Administration program
                        2   // Service Application ID
                    },
                    {
                        DateTime.Parse("2024-01-10"), 
                        DateTime.Parse("2026-06-30"), 
                        "Transfer student with 60 credits", 
                        3,  // Student ID
                        3,  // Electrical Engineering program
                        3   // Service Application ID
                    },
                    // Completed enrollments
                    {
                        DateTime.Parse("2020-09-15"), 
                        DateTime.Parse("2024-05-15"), 
                        "Graduated with distinction", 
                        4,  // Student ID
                        1,  // Computer Science program
                        4   // Service Application ID
                    },
                    {
                        DateTime.Parse("2019-09-15"), 
                        DateTime.Parse("2023-05-20"), 
                        "Valedictorian", 
                        5,  // Student ID
                        2,  // Business Administration program
                        5   // Service Application ID
                    },
                    // Future graduation
                    {
                        DateTime.Parse("2024-09-15"), 
                        DateTime.Parse("2028-06-30"), 
                        "First-year student", 
                        6,  // Student ID
                        2,  // Medicine program
                        6   // Service Application ID
                    }
                });
    
            migrationBuilder.CreateIndex(
                name: "IX_enrollments_program_id",
                table: "enrollments",
                column: "program_id");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_service_application_id",
                table: "enrollments",
                column: "service_application_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_enrollments_student_id",
                table: "enrollments",
                column: "student_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enrollments");
        }
    }
}
