using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDocsVerificationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "docs_verifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    submission_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    verification_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    is_approved = table.Column<bool>(type: "boolean", nullable: true),
                    rejected_reason = table.Column<string>(type: "varchar", maxLength: 200, nullable: true),
                    paid_fees = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    notes = table.Column<string>(type: "varchar", maxLength: 500, nullable: true),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                    verified_by_user_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_docs_verifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_docs_verifications_people_person_id",
                        column: x => x.person_id,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_docs_verifications_users_verified_by_user_id",
                        column: x => x.verified_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_docs_verifications_person_id",
                table: "docs_verifications",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_docs_verifications_verified_by_user_id",
                table: "docs_verifications",
                column: "verified_by_user_id");
            
             migrationBuilder.InsertData(
        table: "docs_verifications",
        columns: new[] { 
            "submission_date", "verification_date", "status", "is_approved", "rejected_reason", "paid_fees", 
            "notes", "person_id", "verified_by_user_id" 
        },
        values: new object[,]
        {
            // Approved verifications
            {
                DateTime.UtcNow.AddDays(-35), 
                DateTime.UtcNow, 
                3, 
                true, 
                null, 
                25.00m, 
                "All documents were valid and matched requirements", 
                1,  // Person ID
                2   // Verified by admin user
            },
            {
                DateTime.UtcNow.AddDays(-15), 
                DateTime.UtcNow.AddDays(8), 
                3, 
                true, 
                null, 
                15.00m, 
                "Quick verification - documents in perfect order", 
                3,  // Person ID
                3   // Verified by staff user
            },
            
            // Rejected verifications
            {
                DateTime.UtcNow.AddMonths(-4), 
                DateTime.UtcNow.AddMonths(-3),  
                4, 
                false, 
                "Expired identification document", 
                25.00m, 
                "Applicant needs to submit updated ID", 
                2,  // Person ID
                2   // Verified by admin user
            },
            {
                new DateTime(2023, 4, 12, 11, 10, 0, DateTimeKind.Utc), 
                new DateTime(2023, 4, 13, 9, 15, 0, DateTimeKind.Utc), 
                4, 
                false, 
                "Missing proof of address", 
                15.00m, 
                "Contacted applicant for additional documents", 
                4,  // Person ID
                3   // Verified by staff user
            },
            
            // Pending verifications
            {
                new DateTime(2023, 5, 1, 14, 30, 0, DateTimeKind.Utc), 
                null, 
                1, 
                false, 
                null, 
                25.00m, 
                "Awaiting manual review", 
                5,  // Person ID
                null // Not yet verified
            },
            {
                new DateTime(2023, 5, 10, 10, 20, 0, DateTimeKind.Utc),  
                null, 
                1, 
                false, 
                null, 
                15.00m, 
                "Documents received, queued for processing", 
                1,  // Person ID (same as first approved)
                null // Not yet verified
            },
            
            // In-progress verification
            {
                new DateTime(2023, 2, 15, 10, 0, 0, DateTimeKind.Utc), 
                null, 
                2, 
                false, 
                null, 
                25.00m, 
                "Currently being reviewed by team", 
                6,  // Person ID
                null // Not yet finalized
            },
            
            // Recently completed verification
            {
                new DateTime(2023, 1, 10, 9, 30, 0, DateTimeKind.Utc), 
                new DateTime(2023, 1, 12, 14, 15, 0, DateTimeKind.Utc),  
                5, 
                true, 
                null, 
                15.00m, 
                "Incomplete document's submitted", 
                7,  // Person ID
                2   // Verified by admin user
            }
        });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "docs_verifications");
        }
    }
}
