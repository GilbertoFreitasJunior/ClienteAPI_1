using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiProj.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    CDFLIFOR = table.Column<int>(type: "int", nullable: false, comment: "Código Cliente")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOCLIFOR = table.Column<string>(type: "varchar(60)", nullable: false, comment: "Nome cliente/Fornecedor")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DDD_FONE = table.Column<short>(type: "smallint(6)", nullable: false, comment: "DDD Fone"),
                    FONE = table.Column<string>(type: "varchar(15)", nullable: true, comment: "Fone")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNPJ_CPF = table.Column<string>(type: "varchar(18)", nullable: true, comment: "CNPJ / CPF")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.CDFLIFOR);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
