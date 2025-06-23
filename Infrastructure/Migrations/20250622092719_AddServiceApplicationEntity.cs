using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceApplicationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "service_applications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    application_date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    paid_fees = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    varchar = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    completed_date = table.Column<DateTime>(type: "date", nullable: true),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                    service_id = table.Column<int>(type: "integer", nullable: false),
                    processed_by_user_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_applications", x => x.id);
                    table.ForeignKey(
                        name: "FK_service_applications_people_person_id",
                        column: x => x.person_id,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_service_applications_service_offers_service_id",
                        column: x => x.service_id,
                        principalTable: "service_offers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_service_applications_users_processed_by_user_id",
                        column: x => x.processed_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_service_applications_person_id",
                table: "service_applications",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_applications_processed_by_user_id",
                table: "service_applications",
                column: "processed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_applications_service_id",
                table: "service_applications",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "ix_service_applications_status",
                table: "service_applications",
                column: "status");
            
            // Seed data for service_applications
    migrationBuilder.InsertData(
        table: "service_applications",
        columns: new[] { 
            "application_date", 
            "status", 
            "paid_fees", 
            "varchar", 
            "completed_date", 
            "person_id", 
            "service_id", 
            "processed_by_user_id" 
        },
        values: new object[,]
        {
            // Completed applications
            {
                DateTime.Parse("2023-01-15"), 
                3, 
                20.00m, 
                "Initial enrollment for Computer Science program", 
                DateTime.Parse("2023-01-20"), 
                1,  // Person ID (assuming this exists)
                1,  // Student Program Enrollment Service
                2   // Processed by admin user
            },
            {
                DateTime.Parse("2023-02-10"), 
                3, 
                10.00m, 
                "Fall 2023 semester registration", 
                DateTime.Parse("2023-02-12"), 
                1,  // Same person
                2,  // Course Registration Service
                2
            },
            {
                DateTime.Parse("2023-03-05"), 
                3, 
                25.00m, 
                "Official transcript for job application", 
                DateTime.Parse("2023-03-07"), 
                2,  // Different person
                5,  // Transcript Issuance Service
                3
            },
            
            // In-progress applications
            {
                DateTime.Parse("2023-04-01"), 
                2, 
                15.00m, 
                "Financial aid application pending documents", 
                null, 
                3,  // Another person
                4,  // Financial Aid Application Service
                null
            },
            {
                DateTime.Parse("2023-04-15"), 
                2, 
                40.00m, 
                "Degree audit for graduation planning", 
                null, 
                1,  // First person again
                7,  // Degree Audit Service
                null
            },
            
            // New applications
            {
                DateTime.Parse("2023-05-01"), 
                1, 
                30.00m, 
                "Lost student ID replacement", 
                null, 
                4,  // New person
                6,  // Student ID Card Service
                null
            },
            {
                DateTime.Parse("2023-05-10"), 
                1, 
                10.00m, 
                "Academic advising for course selection", 
                null, 
                2,  // Second person
                8,  // Academic Advisory Service
                null
            },
            
            // Cancelled application
            {
                DateTime.Parse("2023-03-20"), 
                4, 
                5.00m, 
                "Cancelled grade report request", 
                null, 
                3, 
                3,  // Grade Management Service
                2
            },
            
            // Rejected application
            {
                DateTime.Parse("2023-04-05"), 
                5, 
                0.00m, 
                "Rejected due to incomplete information", 
                null, 
                5, 
                1,  // Student Program Enrollment
                3
            }
        });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "service_applications");
        }
    }
}
