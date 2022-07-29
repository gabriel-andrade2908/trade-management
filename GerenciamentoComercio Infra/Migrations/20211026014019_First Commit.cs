using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciamentoComercio_Infra.Migrations
{
    public partial class FirstCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "latecher");

            migrationBuilder.CreateTable(
                name: "ACCESS",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRIPTION = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    TYPE = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCESS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CLIENT",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FULL_NAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    EMAIL = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CPF = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    PHONE = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    ADDRESS = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FULL_NAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ACCESS = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    PASSWORD = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    EMAIL = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    IS_ADMINISTRATOR = table.Column<bool>(type: "bit", nullable: true),
                    PHONE = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ADDRESS = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_CATEGORY",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_CATEGORY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SERVICE_CATEGORY",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICE_CATEGORY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CLIENT_TRANSACTION",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CLIENT = table.Column<int>(type: "int", nullable: true),
                    ID_EMPLOYEE = table.Column<int>(type: "int", nullable: true),
                    TOTAL_PRICE = table.Column<decimal>(type: "money", nullable: true),
                    DISCOUNT_PRICE = table.Column<decimal>(type: "money", nullable: true),
                    DISCOUNT_PERCENTAGE = table.Column<double>(type: "float", nullable: true),
                    OBSERVATIONS = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENT_TRANSACTION", x => x.ID);
                    table.ForeignKey(
                        name: "FK__CLIENT_TR__ID_CL__36B12243",
                        column: x => x.ID_CLIENT,
                        principalSchema: "latecher",
                        principalTable: "CLIENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CLIENT_TR__ID_EM__37A5467C",
                        column: x => x.ID_EMPLOYEE,
                        principalSchema: "latecher",
                        principalTable: "EMPLOYEE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE_ACCESS",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_EMPLOYEE = table.Column<int>(type: "int", nullable: true),
                    ID_ACCESS = table.Column<int>(type: "int", nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE_ACCESS", x => x.ID);
                    table.ForeignKey(
                        name: "FK__EMPLOYEE___ID_AC__3D5E1FD2",
                        column: x => x.ID_ACCESS,
                        principalSchema: "latecher",
                        principalTable: "ACCESS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__EMPLOYEE___ID_EM__3C69FB99",
                        column: x => x.ID_EMPLOYEE,
                        principalSchema: "latecher",
                        principalTable: "EMPLOYEE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ID_PRODUCT_CATEGORY = table.Column<int>(type: "int", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.ID);
                    table.ForeignKey(
                        name: "FK__PRODUCT__ID_PROD__2E1BDC42",
                        column: x => x.ID_PRODUCT_CATEGORY,
                        principalSchema: "latecher",
                        principalTable: "PRODUCT_CATEGORY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SERVICE",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ID_SERVICE_CATEGORY = table.Column<int>(type: "int", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true),
                    SLA = table.Column<decimal>(type: "money", nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICE", x => x.ID);
                    table.ForeignKey(
                        name: "FK__SERVICE__ID_SERV__2B3F6F97",
                        column: x => x.ID_SERVICE_CATEGORY,
                        principalSchema: "latecher",
                        principalTable: "SERVICE_CATEGORY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CLIENT_TRANSACTION_PRODUCT",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CLIENT_TRANSACTION = table.Column<int>(type: "int", nullable: true),
                    ID_PRODUCT = table.Column<int>(type: "int", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: true),
                    PRICE = table.Column<decimal>(type: "money", nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENT_TRANSACTION_PRODUCT", x => x.ID);
                    table.ForeignKey(
                        name: "FK__CLIENT_TR__ID_CL__440B1D61",
                        column: x => x.ID_CLIENT_TRANSACTION,
                        principalSchema: "latecher",
                        principalTable: "CLIENT_TRANSACTION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CLIENT_TR__ID_PR__44FF419A",
                        column: x => x.ID_PRODUCT,
                        principalSchema: "latecher",
                        principalTable: "PRODUCT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_HISTORIC",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USER = table.Column<int>(type: "int", nullable: true),
                    ID_PRODUCT = table.Column<int>(type: "int", nullable: true),
                    PRICE = table.Column<decimal>(type: "money", nullable: true),
                    QUANTITY = table.Column<long>(type: "bigint", nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_HISTORIC", x => x.ID);
                    table.ForeignKey(
                        name: "FK__PRODUCT_H__ID_PR__33D4B598",
                        column: x => x.ID_PRODUCT,
                        principalSchema: "latecher",
                        principalTable: "PRODUCT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CLIENT_TRANSACTION_SERVICE",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CLIENT_TRANSACTION = table.Column<int>(type: "int", nullable: true),
                    ID_SERVICE = table.Column<int>(type: "int", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: true),
                    PRICE = table.Column<decimal>(type: "money", nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENT_TRANSACTION_SERVICE", x => x.ID);
                    table.ForeignKey(
                        name: "FK__CLIENT_TR__ID_CL__403A8C7D",
                        column: x => x.ID_CLIENT_TRANSACTION,
                        principalSchema: "latecher",
                        principalTable: "CLIENT_TRANSACTION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CLIENT_TR__ID_SE__412EB0B6",
                        column: x => x.ID_SERVICE,
                        principalSchema: "latecher",
                        principalTable: "SERVICE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SERVICE_HISTORIC",
                schema: "latecher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USER = table.Column<int>(type: "int", nullable: true),
                    ID_SERVICE = table.Column<int>(type: "int", nullable: true),
                    SLA = table.Column<decimal>(type: "money", nullable: true),
                    PRICE = table.Column<decimal>(type: "money", nullable: true),
                    CREATION_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATION_USER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERVICE_HISTORIC", x => x.ID);
                    table.ForeignKey(
                        name: "FK__SERVICE_H__ID_SE__30F848ED",
                        column: x => x.ID_SERVICE,
                        principalSchema: "latecher",
                        principalTable: "SERVICE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_TRANSACTION_ID_CLIENT",
                schema: "latecher",
                table: "CLIENT_TRANSACTION",
                column: "ID_CLIENT");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_TRANSACTION_ID_EMPLOYEE",
                schema: "latecher",
                table: "CLIENT_TRANSACTION",
                column: "ID_EMPLOYEE");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_TRANSACTION_PRODUCT_ID_CLIENT_TRANSACTION",
                schema: "latecher",
                table: "CLIENT_TRANSACTION_PRODUCT",
                column: "ID_CLIENT_TRANSACTION");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_TRANSACTION_PRODUCT_ID_PRODUCT",
                schema: "latecher",
                table: "CLIENT_TRANSACTION_PRODUCT",
                column: "ID_PRODUCT");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_TRANSACTION_SERVICE_ID_CLIENT_TRANSACTION",
                schema: "latecher",
                table: "CLIENT_TRANSACTION_SERVICE",
                column: "ID_CLIENT_TRANSACTION");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_TRANSACTION_SERVICE_ID_SERVICE",
                schema: "latecher",
                table: "CLIENT_TRANSACTION_SERVICE",
                column: "ID_SERVICE");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_ACCESS_ID_ACCESS",
                schema: "latecher",
                table: "EMPLOYEE_ACCESS",
                column: "ID_ACCESS");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_ACCESS_ID_EMPLOYEE",
                schema: "latecher",
                table: "EMPLOYEE_ACCESS",
                column: "ID_EMPLOYEE");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_ID_PRODUCT_CATEGORY",
                schema: "latecher",
                table: "PRODUCT",
                column: "ID_PRODUCT_CATEGORY");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_HISTORIC_ID_PRODUCT",
                schema: "latecher",
                table: "PRODUCT_HISTORIC",
                column: "ID_PRODUCT");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICE_ID_SERVICE_CATEGORY",
                schema: "latecher",
                table: "SERVICE",
                column: "ID_SERVICE_CATEGORY");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICE_HISTORIC_ID_SERVICE",
                schema: "latecher",
                table: "SERVICE_HISTORIC",
                column: "ID_SERVICE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENT_TRANSACTION_PRODUCT",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "CLIENT_TRANSACTION_SERVICE",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "EMPLOYEE_ACCESS",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "PRODUCT_HISTORIC",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "SERVICE_HISTORIC",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "CLIENT_TRANSACTION",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "ACCESS",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "PRODUCT",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "SERVICE",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "CLIENT",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "EMPLOYEE",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "PRODUCT_CATEGORY",
                schema: "latecher");

            migrationBuilder.DropTable(
                name: "SERVICE_CATEGORY",
                schema: "latecher");
        }
    }
}
