using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LyfegenContracts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrandedProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandedProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CancerStage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicinalProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    StrengthMgPerMl = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    BrandedProductId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinalProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicinalProducts_BrandedProducts_BrandedProductId",
                        column: x => x.BrandedProductId,
                        principalTable: "BrandedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandedProductId = table.Column<long>(type: "bigint", nullable: false),
                    OSMonths = table.Column<int>(type: "integer", nullable: false),
                    PFSMonths = table.Column<int>(type: "integer", nullable: false),
                    OsAfterMonthsRate = table.Column<int>(type: "integer", nullable: false),
                    OsBeforeMonthsRate = table.Column<int>(type: "integer", nullable: false),
                    PfsAfterMonthsRate = table.Column<int>(type: "integer", nullable: false),
                    PfsBeforeMonthsRate = table.Column<int>(type: "integer", nullable: false),
                    MinStage = table.Column<int>(type: "integer", nullable: false),
                    MaxStage = table.Column<int>(type: "integer", nullable: false),
                    MaxAgeExclusive = table.Column<int>(type: "integer", nullable: false),
                    ManufacturerId = table.Column<long>(type: "bigint", nullable: false),
                    PayerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_BrandedProducts_BrandedProductId",
                        column: x => x.BrandedProductId,
                        principalTable: "BrandedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Payers_PayerId",
                        column: x => x.PayerId,
                        principalTable: "Payers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPacks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicinalProductId = table.Column<long>(type: "bigint", nullable: false),
                    Units = table.Column<int>(type: "integer", nullable: false),
                    BasePriceChf = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPacks_MedicinalProducts_MedicinalProductId",
                        column: x => x.MedicinalProductId,
                        principalTable: "MedicinalProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    ProductPackId = table.Column<long>(type: "bigint", nullable: false),
                    StartDateUtc = table.Column<DateOnly>(type: "date", nullable: false),
                    DurationMonths = table.Column<int>(type: "integer", nullable: false),
                    ContractId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatments_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatments_ProductPacks_ProductPackId",
                        column: x => x.ProductPackId,
                        principalTable: "ProductPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentOutcomes",
                columns: table => new
                {
                    TreatmentId = table.Column<long>(type: "bigint", nullable: false),
                    ProgressionDateUtc = table.Column<DateOnly>(type: "date", nullable: true),
                    DeathDateUtc = table.Column<DateOnly>(type: "date", nullable: true),
                    PaymentRate = table.Column<int>(type: "integer", nullable: false),
                    PayableAmountChf = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    RefundAmountChf = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentOutcomes", x => x.TreatmentId);
                    table.ForeignKey(
                        name: "FK_TreatmentOutcomes_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BrandedProductId",
                table: "Contracts",
                column: "BrandedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ManufacturerId",
                table: "Contracts",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PayerId",
                table: "Contracts",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinalProducts_BrandedProductId",
                table: "MedicinalProducts",
                column: "BrandedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPacks_MedicinalProductId",
                table: "ProductPacks",
                column: "MedicinalProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_ContractId",
                table: "Treatments",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PatientId",
                table: "Treatments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_ProductPackId",
                table: "Treatments",
                column: "ProductPackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreatmentOutcomes");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "ProductPacks");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Payers");

            migrationBuilder.DropTable(
                name: "MedicinalProducts");

            migrationBuilder.DropTable(
                name: "BrandedProducts");
        }
    }
}
