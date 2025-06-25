using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntraceExamEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entrance_exams",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    exam_date = table.Column<DateTime>(type: "date", nullable: false),
                    score = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    max_score = table.Column<int>(type: "integer", nullable: false),
                    passing_score = table.Column<int>(type: "integer", nullable: false),
                    is_passed = table.Column<bool>(type: "boolean", nullable: true),
                    paid_fees = table.Column<decimal>(type: "numeric(10,2)", nullable: true, defaultValue: 50.00m),
                    exam_status = table.Column<int>(type: "integer", nullable: false),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entrance_exams", x => x.id);
                });
                  migrationBuilder.InsertData(
                table: "entrance_exams",
                columns: new[] { 
                    "exam_date", 
                    "score", 
                    "max_score", 
                    "passing_score", 
                    "is_passed", 
                    "paid_fees", 
                    "exam_status", 
                    "notes" 
                },
                values: new object[,]
                {
                    // Scheduled exam (not taken yet)
                    {
                        DateTime.Now.AddDays(7), // exam_date
                        null,                   // score (not taken yet)
                        100,                    // max_score
                        70.0m,                  // passing_score
                        null,                   // is_passed
                        50.00m,                 // paid_fees
                        1, // exam_status
                        "First attempt - scheduled for next week" // notes
                    },

                    // Completed but not scored
                    {
                        DateTime.Now.AddDays(-1),
                        null,
                        100,
                        75.0m,
                        null,
                        50.00m,
                        3,
                        "Completed yesterday, awaiting grading"
                    },

                    // Passed exam (scored 85/100)
                    {
                        DateTime.Now.AddDays(-3),
                        85.0m,
                        100,
                        70.0m,
                        true,
                        50.00m,
                        4,
                        "Excellent performance"
                    },

                    // Failed exam (scored 65/100)
                    {
                        DateTime.Now.AddDays(-5),
                        65.0m,
                        100,
                        70.0m,
                        false,
                        50.00m,
                        4,
                        "Needs improvement in math section"
                    },

                    // No-show exam
                    {
                        DateTime.Now.AddDays(-2),
                        null,
                        100,
                        70.0m,
                        null,
                        50.00m,
                        5,
                        "Student didn't arrive for exam"
                    },

                    // Cancelled exam
                    {
                        DateTime.Now.AddDays(-4),
                        null,
                        100,
                        70.0m,
                        null,
                        0.00m, // No fees charged
                        6,
                        "Cancelled due to weather conditions"
                    },

                    // In-progress exam (for online exams)
                    {
                        DateTime.Now,
                        null,
                        100,
                        80.0m,
                        null,
                        50.00m,
                        2,
                        "Currently being taken - started 30 mins ago"
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entrance_exams");
        }
    }
}
