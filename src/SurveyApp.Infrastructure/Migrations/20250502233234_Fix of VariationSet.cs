using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixofVariationSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VariationSets_QuestionId",
                table: "VariationSets");

            migrationBuilder.DropColumn(
                name: "VariationSetId",
                table: "Questions");

            migrationBuilder.CreateIndex(
                name: "IX_VariationSets_QuestionId",
                table: "VariationSets",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VariationSets_QuestionId",
                table: "VariationSets");

            migrationBuilder.AddColumn<Guid>(
                name: "VariationSetId",
                table: "Questions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VariationSets_QuestionId",
                table: "VariationSets",
                column: "QuestionId",
                unique: true);
        }
    }
}
