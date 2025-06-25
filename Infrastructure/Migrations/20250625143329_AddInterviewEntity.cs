using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInterviewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "interviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scheduled_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actual_start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    actual_end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_approved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    paid_fees = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    recommendation = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    professor_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interviews", x => x.id);
                    table.ForeignKey(
                        name: "FK_interviews_professors_professor_id",
                        column: x => x.professor_id,
                        principalTable: "professors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_interviews_professor_id",
                table: "interviews",
                column: "professor_id");
            // Seed data for interviews
    migrationBuilder.InsertData(
        table: "interviews",
        columns: new[] { 
            "scheduled_date_time", 
            "actual_start_time", 
            "actual_end_time", 
            "is_approved", 
            "paid_fees", 
            "notes", 
            "recommendation", 
            "professor_id" 
        },
        values: new object[,]
        {
            // Upcoming interview (not yet conducted)
            {
                DateTime.UtcNow.AddDays(2), // scheduled_date_time
                null, // actual_start_time
                null, // actual_end_time
                false, // is_approved
                40.00m, // paid_fees
                "Candidate requested morning time slot", // notes
                null, // recommendation
                1 // professor_id 
            },

            // Completed approved interview
            {
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow.AddDays(-3).AddMinutes(5),
                DateTime.UtcNow.AddDays(-3).AddMinutes(45),
                true,
                40.00m,
                "Candidate arrived 10 minutes early",
                "Exceptional candidate with strong research background. Highly recommend for AI specialization.",
                2
            },

            // Completed rejected interview
            {
                DateTime.UtcNow.AddDays(-1),
                DateTime.UtcNow.AddDays(-1).AddMinutes(15),
                DateTime.UtcNow.AddDays(-1).AddMinutes(30),
                false,
                40.00m,
                "Technical skills below program requirements",
                "Candidate struggled with basic algorithms questions. Suggest reapplying after completing additional coursework.",
                3
            },

            // No-show interview
            {
                DateTime.UtcNow.AddDays(-5),
                null,
                null,
                false,
                0.00m,
                "Candidate did not arrive or contact us",
                null,
                1
            },

            // Completed interview awaiting decision
            {
                DateTime.UtcNow.AddHours(-2),
                DateTime.UtcNow.AddHours(-2),
                DateTime.UtcNow.AddHours(-1),
                false,
                40.00m,
                "Need to compare with other candidates",
                "Solid performance but not outstanding. Compare with remaining pool before deciding.",
                2
            },

            // Interview in progress (started but not ended)
            {
                DateTime.UtcNow.AddMinutes(-30),
                DateTime.UtcNow.AddMinutes(-25),
                null,
                false,
                40.00m,
                "Currently being conducted in Room 205",
                null,
                3
            },

            // Scholarship candidate interview
            {
                DateTime.UtcNow.AddDays(-7),
                DateTime.UtcNow.AddDays(-7),
                DateTime.UtcNow.AddDays(-7).AddMinutes(60),
                true,
                40.00m,
                "Scholarship committee review required",
                "Top 5% candidate. Recommend full tuition waiver and research assistantship.",
                1
            }
        });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "interviews");
        }
    }
}
