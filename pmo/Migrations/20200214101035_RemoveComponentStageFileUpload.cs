using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class RemoveComponentStageFileUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadedDocumentation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadedDocumentation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDesignApprovalId = table.Column<int>(type: "int", nullable: true),
                    GateId = table.Column<int>(type: "int", nullable: true),
                    ProductInfrigmentPatentabilityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedDocumentation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadedDocumentation_CustomerDesignApprovals_CustomerDesignApprovalId",
                        column: x => x.CustomerDesignApprovalId,
                        principalTable: "CustomerDesignApprovals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadedDocumentation_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadedDocumentation_ProductInfrigmentPatentabilities_ProductInfrigmentPatentabilityId",
                        column: x => x.ProductInfrigmentPatentabilityId,
                        principalTable: "ProductInfrigmentPatentabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadedDocumentation_CustomerDesignApprovalId",
                table: "UploadedDocumentation",
                column: "CustomerDesignApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedDocumentation_GateId",
                table: "UploadedDocumentation",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedDocumentation_ProductInfrigmentPatentabilityId",
                table: "UploadedDocumentation",
                column: "ProductInfrigmentPatentabilityId");
        }
    }
}
